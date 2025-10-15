import { Routes, Route } from 'react-router-dom'; 
import Home from './pages/Home.tsx'; 

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />  // Route trang chủ
      {/* <Route path="*" element={<NotFound />} />  // Route 404 (bắt tất cả) */}
    </Routes>
  );
}

export default App;
