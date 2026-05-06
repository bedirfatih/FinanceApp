import { useEffect, useState } from 'react';
import api from '../api';

export default function TransactionList() {
  const [transactions, setTransactions] = useState([]);
  const [userId, setUserId] = useState('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchTransactions = (uid) => {
    setLoading(true);
    setError(null);
    const url = uid ? `/transactions/user/${uid}` : '/transactions';
    api.get(url)
      .then(res => setTransactions(res.data))
      .catch(() => setError('Failed to load transactions.'))
      .finally(() => setLoading(false));
  };

  useEffect(() => { fetchTransactions(''); }, []);

  const handleFilter = (e) => {
    e.preventDefault();
    fetchTransactions(userId);
  };

  const handleClear = () => {
    setUserId('');
    fetchTransactions('');
  };

  return (
    <div>
      <h1>Transactions</h1>

      <form className="filter-bar" onSubmit={handleFilter}>
        <input
          type="number"
          placeholder="Filter by User ID"
          value={userId}
          onChange={e => setUserId(e.target.value)}
          min="1"
        />
        <button type="submit">Filter</button>
        <button type="button" onClick={handleClear}>Clear</button>
      </form>

      {loading && <p className="status-msg">Loading...</p>}
      {error && <p className="status-msg error">{error}</p>}

      {!loading && !error && (
        transactions.length === 0 ? (
          <p className="status-msg">No transactions found.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>ID</th>
                <th>User ID</th>
                <th>Amount</th>
                <th>Category</th>
                <th>Description</th>
                <th>Date</th>
              </tr>
            </thead>
            <tbody>
              {transactions.map(t => (
                <tr key={t.id}>
                  <td>{t.id}</td>
                  <td>{t.userId}</td>
                  <td>{t.amount.toFixed(2)}</td>
                  <td>{t.category}</td>
                  <td>{t.description}</td>
                  <td>{new Date(t.date).toLocaleDateString()}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )
      )}
    </div>
  );
}
