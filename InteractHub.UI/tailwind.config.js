/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        bui: {
          bg: '#F4F1EA',       // Màu Be/Kem sáng (như màu giấy cũ)
          card: '#EBE5D9',     // Màu Be đậm hơn xíu cho cái form
          primary: '#1A3622',  // Xanh rêu/Xanh cổ vịt đậm (chuẩn màu chữ logo)
          border: '#1A3622',   // Viền xanh rêu
          text: '#1A3622',     // Chữ xanh rêu
          sub: '#4A6B53',      // Xanh rêu nhạt cho nhãn/placeholder
        }
      },
      boxShadow: {
        'bui-hard': '6px 6px 0px #1A3622', // Bóng đổ khối cứng màu xanh rêu
      }
    },
  },
  plugins: [],
}