import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import AvaliarPage from './pages/AvaliarPage';


export default function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/avaliar" element={<AvaliarPage />} />

      </Routes>
    </Router>
  );
}
