export const API = import.meta.env.VITE_API_BASE_URL || "https://localhost:7219/api/";
export const API_LOCAL = import.meta.env.VITE_API_LOCAL_BASE_URL;
export const API_LOCAL_CATEGORY = import.meta.env.VITE_API_LOCAL_CATEGORY;
export const endpoints = {
   auth: {
    login: "Authentication/login",
    adminLogin: "Authentication/admin-login",
    me: "Authentication/me",
    sendEmail: "Authentication/send-confirmation-code",
    confirmEmail: "Authentication/confirm-email"
  },
  branches: {
    all: "Branch/filter",
    getBranchById: (id: string) => `Branch/${id}`,
    createBranch:"Branch",
    updateBranch:"Branch",
    deleteBranch:(id:string)=>`Branch/${id}`,
  },
  security: {
    profile: `security/profile`,
  },
  main: {
    roomManager:{
      getAll:`roomManager/searchAll`,
      insert: `roomManager/saveRoom`,
      getDataById: `roomManager/get-data-by-id`,
      delete: `roomManager/deleteById`,
    },
  },
};
