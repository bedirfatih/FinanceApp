import { useEffect, useState } from 'react';
import api from '../api';

const EMPTY_FORM = { fromUserId: '', toUserId: '', amount: '' };

export default function TransferForm() {
  const [users, setUsers] = useState([]);
  const [form, setForm] = useState(EMPTY_FORM);
  const [result, setResult] = useState(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    api.get('/users').then(res => setUsers(res.data)).catch(console.error);
  }, []);

  const set = (field) => (e) => setForm(prev => ({ ...prev, [field]: e.target.value }));

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setResult(null);
    try {
      const res = await api.post('/transfers', {
        fromUserId: Number(form.fromUserId),
        toUserId: Number(form.toUserId),
        amount: Number(form.amount),
      });
      setResult(res.data);
      setForm(EMPTY_FORM);
    } catch (err) {
      setResult({ status: 'Error', message: err.message });
    } finally {
      setLoading(false);
    }
  };

  const statusClass =
    result?.status === 'Completed' ? 'status-completed' :
    result?.status === 'Failed' ? 'status-failed' : '';

  return (
    <div>
      <h1>New Transfer</h1>

      <form onSubmit={handleSubmit}>
        <div>
          <label>From</label>
          <select value={form.fromUserId} onChange={set('fromUserId')} required>
            <option value="">Select sender</option>
            {users.map(u => (
              <option key={u.id} value={u.id}>
                {u.name} — Balance: {u.balance.toFixed(2)}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label>To</label>
          <select value={form.toUserId} onChange={set('toUserId')} required>
            <option value="">Select recipient</option>
            {users.map(u => (
              <option key={u.id} value={u.id}>{u.name}</option>
            ))}
          </select>
        </div>

        <div>
          <label>Amount</label>
          <input
            type="number"
            min="0.01"
            step="0.01"
            placeholder="0.00"
            value={form.amount}
            onChange={set('amount')}
            required
          />
        </div>

        <button type="submit" disabled={loading}>
          {loading ? 'Processing…' : 'Send Transfer'}
        </button>
      </form>

      {result && (
        <div className="result-box">
          <p>Status: <span className={statusClass}>{result.status}</span></p>
          {result.amount !== undefined && <p>Amount: {result.amount}</p>}
          {result.message && <p>{result.message}</p>}
        </div>
      )}
    </div>
  );
}
