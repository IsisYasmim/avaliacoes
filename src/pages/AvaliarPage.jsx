import { useState } from 'react';
import API from '../services/api';
import { useNavigate } from 'react-router-dom';

export default function AvaliarPage() {
  const [eventoId, setEventoId] = useState('');
  const [palestranteId, setPalestranteId] = useState('');
  const [pontuacao, setPontuacao] = useState('');
  const [comentario, setComentario] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  async function enviarAvaliacao(e) {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      const token = localStorage.getItem('token');
      if (!token) {
        throw new Error('Usuário não autenticado');
      }
      const apiUrl = 'http://localhost:5003/api/avaliacoes-service';
      const response = await API.post(apiUrl, {
        eventoId: Number(eventoId),
        palestranteId: Number(palestranteId),
        pontuacao: Number(pontuacao),
        comentario,
      }, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });

      alert('Avaliação enviada com sucesso!');
      navigate('/');
    } catch (err) {
      const errorMessage = err.response?.data?.message || 
                         err.response?.data || 
                         err.message || 
                         'Erro ao enviar avaliação';
      setError(errorMessage);
      console.error('Detalhes do erro:', err.response?.data);
    } finally {
      setLoading(false);
    }
  }

  return (
    <form onSubmit={enviarAvaliacao}>
      <h2>Avaliar</h2>
      {error && <div style={{color: 'red'}}>{error}</div>}
      
      <input 
        placeholder="Evento ID" 
        value={eventoId} 
        onChange={e => setEventoId(e.target.value)} 
        type="number"
        required
      />
      
      <input 
        placeholder="Palestrante ID" 
        value={palestranteId} 
        onChange={e => setPalestranteId(e.target.value)}
        type="number"
        required
      />
      
      <input 
        placeholder="Pontuação (1-5)" 
        type="number" 
        value={pontuacao} 
        onChange={e => setPontuacao(e.target.value)}
        min="1"
        max="5"
        required
      />
      
      <textarea 
        placeholder="Comentário" 
        value={comentario} 
        onChange={e => setComentario(e.target.value)}
        required
      />
      
      <button type="submit" disabled={loading}>
        {loading ? 'Enviando...' : 'Enviar'}
      </button>
    </form>
  );
}