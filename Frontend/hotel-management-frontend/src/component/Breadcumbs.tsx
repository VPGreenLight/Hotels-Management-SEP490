import { Link, useLocation } from "react-router-dom";

const Breadcrumbs: React.FC = () => {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter((x) => x);

  return (
    <nav
      className="flex text-sm text-gray-600 mb-4 px-6 py-2 bg-gray-50 border-b border-gray-200"
      aria-label="Breadcrumb"
    >
      <ol className="inline-flex items-center space-x-1">
       
        <li>
          <Link to="/dashboard" className="text-gray-500 hover:text-indigo-600">
            Dashboard
          </Link>
          {pathnames.length > 0 && <span className="mx-2">/</span>}
        </li>

     
        {pathnames.map((value, index) => {
          const to = `/${pathnames.slice(0, index + 1).join("/")}`;
          const isLast = index === pathnames.length - 1;

          return (
            <li key={to} className="inline-flex items-center">
              {isLast ? (
                <span className="text-gray-800 font-medium capitalize">
                  {decodeURIComponent(value)}
                </span>
              ) : (
                <>
                  <Link
                    to={to}
                    className="text-gray-500 hover:text-indigo-600 capitalize"
                  >
                    {decodeURIComponent(value)}
                  </Link>
                  <span className="mx-2">/</span>
                </>
              )}
            </li>
          );
        })}
      </ol>
    </nav>
  );
};

export default Breadcrumbs;
