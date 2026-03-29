import { useState } from 'react';
import { Link } from 'react-router-dom';
import axiosClient from '../api/axiosClient';
import { isAxiosError } from 'axios';

export default function Register() {
  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    userName: '',
    password: '',
  });
  
  const [message, setMessage] = useState('');
  const [isSuccess, setIsSuccess] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage('ĐANG XỬ LÝ...'); 
    
    try {
      const response = await axiosClient.post('/Auth/register', formData);
      setMessage("🎉 " + response.data.message.toUpperCase()); 
      setIsSuccess(true);
    } catch (error) {
      let errorMsg = "ĐĂNG KÝ THẤT BẠI, VUI LÒNG KIỂM TRA LẠI MẬT KHẨU HOẶC EMAIL!";
      
      if (isAxiosError(error)) {
        errorMsg = error.response?.data?.[0]?.description?.toUpperCase() || errorMsg;
      }

      setMessage("❌ " + errorMsg);
      setIsSuccess(false);
    }
  };

  // STYLE CHUẨN VINTAGE: Nền sáng, viền rêu, chữ rêu
  const inputStyle = "w-full px-4 py-3 bg-bui-bg border-2 border-bui-border text-bui-text rounded-none focus:outline-none focus:bg-white transition placeholder:text-bui-sub/50 font-semibold";
  const labelStyle = "block text-bui-primary text-xs font-bold mb-1.5 tracking-widest uppercase";

  return (
    <div className="min-h-screen bg-bui-bg flex items-center justify-center p-4">
      
      <div className="bg-bui-card p-8 rounded-none border-2 border-bui-border shadow-bui-hard max-w-md w-full">
        
        {/* Dùng font-serif để giống nét chữ trên Logo Bụi District */}
        <h2 className="text-5xl font-black text-center text-bui-primary mb-2 tracking-tighter font-serif">Bụi District</h2>
        <p className="text-center text-bui-primary/80 mb-8 text-sm tracking-widest font-bold border-b border-bui-border pb-4">INTERACT HUB</p>
        
        {message && (
          <div className={`p-3 mb-6 rounded-none border-2 text-sm font-bold tracking-wide ${isSuccess ? 'bg-[#D1E7DD] border-[#0F5132] text-[#0F5132]' : 'bg-[#F8D7DA] border-[#842029] text-[#842029]'}`}>
            {message}
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-5">
          <div>
            <label className={labelStyle}>Họ và Tên</label>
            <input type="text" name="fullName" onChange={handleChange} required placeholder="Nguyễn Văn A"
              className={inputStyle} />
          </div>
          <div>
            <label className={labelStyle}>Email</label>
            <input type="email" name="email" onChange={handleChange} required placeholder="a@email.com"
              className={inputStyle} />
          </div>
          <div>
            <label className={labelStyle}>Tên đăng nhập (Username)</label>
            <input type="text" name="userName" onChange={handleChange} required placeholder="vana_bui"
              className={inputStyle} />
          </div>
          <div>
            <label className={labelStyle}>Mật khẩu</label>
            <input type="password" name="password" onChange={handleChange} required placeholder="********"
              className={inputStyle} />
          </div>
          
          {/* Nút bấm: Nền rêu, chữ be */}
          <button type="submit" className="w-full bg-bui-primary text-bui-bg font-bold py-3 px-4 rounded-none hover:bg-bui-text hover:shadow-[4px_4px_0px_#000] border-2 border-bui-border transition duration-200 uppercase tracking-widest text-lg">
            Gia Nhập Ngay
          </button>
        </form>

        <p className="text-center text-sm text-bui-primary mt-6 tracking-wide font-bold">
          ĐÃ CÓ TÀI KHOẢN? <Link to="/login" className="text-bui-primary hover:text-black underline">ĐĂNG NHẬP</Link>
        </p>
      </div>
    </div>
  );
}