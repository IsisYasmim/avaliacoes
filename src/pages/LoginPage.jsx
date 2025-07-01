import { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

export default function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  async function login(e) {
  e.preventDefault();
  setLoading(true);
  setError('');
  
  try {
    // Use this for Docker-to-Docker communication (if frontend is served from container)
    // const apiUrl = '/api/auth/login';
    
    // Use this for development (access from browser)
    const apiUrl = 'http://localhost:5001/api/auth/login';

    const response = await axios.post(apiUrl, {
      email,
      password,
    }, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    
    localStorage.setItem('token', response.data.token);
    navigate('/avaliar');
  } catch (err) {
      if (err.response) {
        // The request was made and the server responded with a status code
        setError(err.response.data.message || 'Credenciais inválidas');
      } else if (err.request) {
        // The request was made but no response was received
        setError('Servidor não respondeu. Verifique sua conexão.');
      } else {
        // Something happened in setting up the request
        setError('Erro ao configurar a requisição');
      }
    } finally {
      setLoading(false);
    }
  }

  return (
    <form onSubmit={login}>
      <h2>Login</h2>
      {error && <div style={{ color: 'red' }}>{error}</div>}
      <input 
        placeholder="Email" 
        value={email} 
        onChange={e => setEmail(e.target.value)} 
        type="email"
        required
      />
      <input 
        placeholder="password" 
        type="password" 
        value={password} 
        onChange={e => setPassword(e.target.value)} 
        required
      />
      <button type="submit" disabled={loading}>
        {loading ? 'Carregando...' : 'Entrar'}
      </button>
    </form>
  );
}