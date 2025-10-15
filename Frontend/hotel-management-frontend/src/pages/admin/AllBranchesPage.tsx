import React, { useEffect, useState } from "react";
import AdminDashboard from "./AdminDashboard";
import { Building2, MapPin, Phone, Mail, User, X, Edit, Trash2 } from "lucide-react";
import { request } from "../../services/api-client";
import { endpoints } from "../../services/endpoints";

interface Branch {
  id: string;
  name: string;
  address: string;
  managerId: string;
  status: number;
  totalRooms: number;
  parkingSlotsCount: number;
  managerName?: string;
}

interface Province {
  code: number;
  name: string;
  districts: { code: number; name: string }[];
}

const AllBranchesPage: React.FC = () => {
  const [branches, setBranches] = useState<Branch[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  const [showForm, setShowForm] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [editingBranchId, setEditingBranchId] = useState<string | null>(null);

  const [formData, setFormData] = useState({
    name: "",
    address: "",
    totalRooms: 0,
    parkingSlotsCount: 0,
  });
  const [error, setError] = useState<string | null>(null);
  const [submitting, setSubmitting] = useState(false);

  const [provinces, setProvinces] = useState<Province[]>([]);
  const [selectedProvince, setSelectedProvince] = useState<string>("");
  const [districts, setDistricts] = useState<{ code: number; name: string }[]>([]);
  const [selectedDistrict, setSelectedDistrict] = useState<string>("");

  // GET ALL BRANCHES
  const fetchBranches = async () => {
    try {
      const res = await request(
        "post",
        endpoints.branches.all,
        {
          data: { query: "", pageNumber: 1, pageSize: 100 },
          timestamp: 0,
        },
        { withCredentials: true }
      );

      if (res.status === 200 && res.responseData?.items) {
        const baseBranches = res.responseData.items as Branch[];

        const detailedBranches = await Promise.all(
          baseBranches.map(async (branch) => {
            try {
              const detailRes = await request(
                "get",
                endpoints.branches.getBranchById(branch.id),
                null,
                { withCredentials: true }
              );

              if (detailRes.status === 200 && detailRes.responseData) {
                const detail = detailRes.responseData;
                return {
                  ...branch,
                  managerName: detail.managerName || "Không có dữ liệu",
                };
              } else {
                return { ...branch, managerName: "Không có dữ liệu" };
              }
            } catch {
              return { ...branch, managerName: "Không thể tải" };
            }
          })
        );

        setBranches(detailedBranches);
      }
    } catch (error) {
      console.error("Lỗi khi lấy danh sách cơ sở:", error);
    } finally {
      setLoading(false);
    }
  };

  // API OPEN
  const fetchProvinces = async () => {
    try {
      const res = await fetch("https://provinces.open-api.vn/api/?depth=2");
      const data = await res.json();
      setProvinces(data);
    } catch (err) {
      console.error("Lỗi khi lấy danh sách tỉnh:", err);
    }
  };

  useEffect(() => {
    fetchBranches();
    fetchProvinces();
  }, []);

  useEffect(() => {
    const province = provinces.find((p) => p.name === selectedProvince);
    if (province) {
      setDistricts(province.districts);
    } else {
      setDistricts([]);
    }
  }, [selectedProvince, provinces]);

  // VALIDATE
  const validateForm = () => {
    if (!formData.name.trim()) return "Tên cơ sở không được để trống";
    if (!selectedProvince) return "Vui lòng chọn Tỉnh/Thành phố";
    if (!selectedDistrict) return "Vui lòng chọn Quận/Huyện";
    if (formData.totalRooms <= 0) return "Tổng số phòng phải lớn hơn 0";
    if (formData.parkingSlotsCount < 0) return "Chỗ đậu xe không được âm";
    return null;
  };

  // CREATE BRANCHES
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const validationError = validateForm();
    if (validationError) {
      setError(validationError);
      return;
    }

    setSubmitting(true);
    setError(null);

    const fullAddress = `${selectedDistrict}, ${selectedProvince}`;

    try {
      if (isEditing && editingBranchId) {
        const res = await request(
          "put",
          `${endpoints.branches.updateBranch}`,
          {
            id: editingBranchId,
            ...formData,
            address: fullAddress,
          },
          { withCredentials: true }
        );

        if (res.status === 200) {
          setShowForm(false);
          setIsEditing(false);
          setEditingBranchId(null);
          fetchBranches();
        } else {
          setError(res?.message || "Không thể cập nhật cơ sở");
        }
      } else {
        const res = await request(
          "post",
          endpoints.branches.createBranch,
          {
            ...formData,
            address: fullAddress,
          },
          { withCredentials: true }
        );

        if (res.status === 200 || res.status === 201) {
          setShowForm(false);
          fetchBranches();
        } else {
          setError(res?.message || "Không thể tạo cơ sở mới");
        }
      }

      setFormData({ name: "", address: "", totalRooms: 0, parkingSlotsCount: 0 });
      setSelectedProvince("");
      setSelectedDistrict("");
    } catch (err: any) {
      setError("Lỗi khi gọi API: " + (err?.message || ""));
    } finally {
      setSubmitting(false);
    }
  };

  // UPDATE BRANCHES
  const handleEdit = async (branch: Branch) => {
    setIsEditing(true);
    setEditingBranchId(branch.id);
    setLoading(true);

    try {
      const res = await request(
        "get",
        endpoints.branches.getBranchById(branch.id),
        null,
        { withCredentials: true }
      );

      if (res.status === 200 && res.responseData) {
        const detail = res.responseData;

        setFormData({
          name: detail.name,
          address: detail.address,
          totalRooms: detail.totalRooms,
          parkingSlotsCount: detail.parkingSlotsCount,
        });

        const parts = detail.address?.split(",").map((x: string) => x.trim()) || [];
        if (parts.length >= 2) {
          setSelectedDistrict(parts[0]);
          setSelectedProvince(parts[1]);
        } else {
          setSelectedProvince("");
          setSelectedDistrict("");
        }

        setShowForm(true);
      } else {
        alert("Không thể tải thông tin chi tiết của cơ sở này!");
      }
    } catch (error) {
      console.error("Lỗi khi tải chi tiết cơ sở:", error);
      alert("Lỗi khi tải chi tiết cơ sở. Vui lòng thử lại!");
    } finally {
      setLoading(false);
    }
  };

  // DELETE BRANCHES
  const handleDelete = async (id: string) => {
    if (!window.confirm("Bạn có chắc chắn muốn xóa cơ sở này không?")) return;

    try {
      const res = await request(
        "delete",
        endpoints.branches.deleteBranch(id),
        null,
        { withCredentials: true }
      );

      if (res.status === 200) {
        alert("Xóa cơ sở thành công!");
        fetchBranches();
      } else {
        alert(res?.message || "Không thể xóa cơ sở!");
      }
    } catch (err: any) {
      alert("Lỗi khi gọi API: " + (err?.message || ""));
    }
  };

  
  return (
    <AdminDashboard>
      <div className="bg-white rounded-xl shadow-md p-6">
        <div className="flex items-center justify-between mb-6">
          <div className="flex items-center">
            <Building2 size={28} className="text-indigo-600 mr-3" />
            <h2 className="text-xl font-semibold text-gray-700">
              Danh sách các cơ sở hiện tại
            </h2>
          </div>
          <button
            onClick={() => {
              setShowForm(true);
              setIsEditing(false);
              setEditingBranchId(null);
              setFormData({
                name: "",
                address: "",
                totalRooms: 0,
                parkingSlotsCount: 0,
              });
              setSelectedProvince("");
              setSelectedDistrict("");
            }}
            className="flex items-center gap-2 bg-indigo-600 hover:bg-indigo-700 text-white px-4 py-2 rounded-lg text-sm font-medium shadow-sm transition"

          >
            ➕ Thêm cơ sở
          </button>

        </div>

        {loading ? (
          <p className="text-gray-500">Đang tải dữ liệu...</p>
        ) : branches.length === 0 ? (
          <p className="text-gray-500 italic">Hiện chưa có cơ sở nào được thêm.</p>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {branches.map((branch) => (
              <div
                key={branch.id}
                className="p-5 border border-gray-200 rounded-xl shadow-sm hover:shadow-lg transition-shadow bg-gradient-to-br from-indigo-50 to-white"
              >
                <div className="flex justify-between items-start">
                  <h3 className="text-lg font-semibold text-indigo-700 mb-2">
                    {branch.name}
                  </h3>
                  <div className="flex gap-2">
                    <button
                      onClick={() => handleEdit(branch)}
                      title="Chỉnh sửa"
                      className="p-1 rounded-full bg-gradient-to-r from-indigo-500 to-blue-600 text-white hover:opacity-90 transition-all"
                    >
                      <Edit size={16} />
                    </button>
                    <button
                      onClick={() => handleDelete(branch.id)}
                      title="Xóa"
                      className="p-1 rounded-full bg-gradient-to-r from-indigo-500 to-blue-600 text-white hover:opacity-90 transition-all"
                    >
                      <Trash2 size={16} />
                    </button>
                  </div>

                </div>

                <div className="space-y-2 text-sm text-gray-600">
                  <p className="flex items-center">
                    <MapPin size={16} className="mr-2 text-gray-400" />
                    {branch.address}
                  </p>
                  <p className="flex items-center">
                    <Phone size={16} className="mr-2 text-gray-400" />
                    Tổng số phòng: {branch.totalRooms}
                  </p>
                  <p className="flex items-center">
                    <Mail size={16} className="mr-2 text-gray-400" />
                    Chỗ đậu xe: {branch.parkingSlotsCount}
                  </p>
                  <p className="flex items-center">
                    <User size={16} className="mr-2 text-gray-400" />
                    Quản lý: {branch.managerName}
                  </p>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>

      {/* === FORM === */}
      {showForm && (
        <div className="fixed inset-0 bg-black/40 backdrop-blur-sm flex justify-center items-center z-50">
          <div className="bg-white rounded-2xl p-8 w-[550px] max-h-[90vh] overflow-y-auto shadow-2xl relative text-gray-800">
            <button
              onClick={() => setShowForm(false)}
              className="absolute top-4 right-4 text-gray-500 hover:text-gray-800"
            >
              <X size={22} />
            </button>

            <h3 className="text-2xl font-semibold mb-6 text-center text-indigo-700">
              {isEditing ? "✏️ Chỉnh sửa cơ sở" : "➕ Thêm cơ sở mới"}
            </h3>

            {error && <div className="text-red-600 mb-3 text-sm">{error}</div>}

            {/* --- FORM --- */}
            <form onSubmit={handleSubmit} className="space-y-5">
              {/* name */}
              <div>
                <label className="block text-sm font-medium mb-1 text-gray-800">
                  Tên cơ sở
                </label>
                <input
                  type="text"
                  value={formData.name}
                  onChange={(e) =>
                    setFormData({ ...formData, name: e.target.value })
                  }
                  className="w-full border border-gray-300 text-gray-800 bg-white p-2.5 rounded-md focus:ring-2 focus:ring-indigo-400 focus:border-indigo-400 outline-none"
                />
              </div>

              {/* Province */}
              <div>
                <label className="block text-sm font-medium mb-1 text-gray-800">
                  Tỉnh/Thành phố
                </label>
                <select
                  value={selectedProvince}
                  onChange={(e) => {
                    setSelectedProvince(e.target.value);
                    setSelectedDistrict("");
                  }}
                  className="w-full border border-gray-300 bg-white p-2.5 rounded-md text-gray-800 focus:ring-2 focus:ring-indigo-400 focus:border-indigo-400 outline-none"
                >
                  <option value="">-- Chọn Tỉnh/Thành phố --</option>
                  {provinces.map((p) => (
                    <option key={p.code} value={p.name}>
                      {p.name}
                    </option>
                  ))}
                </select>
              </div>

              {/* District */}
              {districts.length > 0 && (
                <div>
                  <label className="block text-sm font-medium mb-1 text-gray-800">
                    Quận/Huyện
                  </label>
                  <select
                    value={selectedDistrict}
                    onChange={(e) => setSelectedDistrict(e.target.value)}
                    className="w-full border border-gray-300 bg-white p-2.5 rounded-md text-gray-800 focus:ring-2 focus:ring-indigo-400 focus:border-indigo-400 outline-none"
                  >
                    <option value="">-- Chọn Quận/Huyện --</option>
                    {districts.map((d) => (
                      <option key={d.code} value={d.name}>
                        {d.name}
                      </option>
                    ))}
                  </select>
                </div>
              )}

              {/* Room + Parking */}
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium mb-1 text-gray-800">
                    Tổng phòng
                  </label>
                  <input
                    type="number"
                    value={formData.totalRooms}
                    onChange={(e) =>
                      setFormData({
                        ...formData,
                        totalRooms: Number(e.target.value),
                      })
                    }
                    className="w-full border border-gray-300 text-gray-800 bg-white p-2.5 rounded-md focus:ring-2 focus:ring-indigo-400 focus:border-indigo-400 outline-none"
                  />
                </div>

                <div>
                  <label className="block text-sm font-medium mb-1 text-gray-800">
                    Chỗ đậu xe
                  </label>
                  <input
                    type="number"
                    value={formData.parkingSlotsCount}
                    onChange={(e) =>
                      setFormData({
                        ...formData,
                        parkingSlotsCount: Number(e.target.value),
                      })
                    }
                    className="w-full border border-gray-300 text-gray-800 bg-white p-2.5 rounded-md focus:ring-2 focus:ring-indigo-400 focus:border-indigo-400 outline-none"
                  />
                </div>
              </div>

              <div className="flex justify-end space-x-3 mt-6">
                <button
                  type="button"
                  onClick={() => setShowForm(false)}
                  className="px-4 py-2 border text-gray-700 rounded-md hover:bg-gray-100"
                >
                  Hủy
                </button>
                <button
                  type="submit"
                  disabled={submitting}
                  className="bg-indigo-600 text-white px-5 py-2 rounded-md hover:bg-indigo-700 disabled:opacity-50"
                >
                  {submitting ? "Đang lưu..." : isEditing ? "Cập nhật" : "Lưu"}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </AdminDashboard>
  );
};

export default AllBranchesPage;
