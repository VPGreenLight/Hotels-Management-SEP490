export const API = import.meta.env.VITE_API_BASE_URL;
export const API_LOCAL = import.meta.env.VITE_API_LOCAL_BASE_URL;
export const API_LOCAL_CATEGORY = import.meta.env.VITE_API_LOCAL_CATEGORY;
export const endpoints = {
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
