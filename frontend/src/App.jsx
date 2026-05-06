import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import TransactionList from './pages/TransactionList';
import TransferForm from './pages/TransferForm';

export default function App() {
  return (
    <BrowserRouter>
      <nav>
        <span className="nav-brand">FinanceApp</span>
        <NavLink to="/" end>Dashboard</NavLink>
        <NavLink to="/transactions">Transactions</NavLink>
        <NavLink to="/transfers">New Transfer</NavLink>
      </nav>
      <main>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/transactions" element={<TransactionList />} />
          <Route path="/transfers" element={<TransferForm />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}
