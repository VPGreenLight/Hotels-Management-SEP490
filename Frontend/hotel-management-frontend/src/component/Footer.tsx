const Footer: React.FC = () => {
  return (
    <footer className="bg-white shadow-inner border-t border-gray-200 mt-6">
      <div className="max-w-7xl mx-auto py-4 px-6 flex flex-col sm:flex-row justify-between items-center text-sm text-gray-500">
        <span>© {new Date().getFullYear()} Tân Trường Sơn Hotel Management System. All rights reserved.</span>
        <div className="flex space-x-4 mt-2 sm:mt-0">
          <a href="p" className="hover:text-blue-600">Privacy Policy</a>
          <a href="a" className="hover:text-blue-600">Terms of Service</a>
          <a href="a" className="hover:text-blue-600">Support</a>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
