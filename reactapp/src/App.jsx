import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom"
import KeycloakRoute from './Hoc/keycloakRoutes';
import { ROLES } from "./const/roles"
import Dashboard from "./Views/Dashboard"
import Login from "./Views/Login"
import Navbar from './Components/navbar/Navbar';

function App() {
    return (
      <BrowserRouter>
        <Navbar />
        <main className="container">
          <Routes>
            <Route path="/" element={<Login />} />
            <Route
              path="/dashboard"
              element={
                <KeycloakRoute role={ ROLES.Regular }>
                  <Dashboard />
                </KeycloakRoute>
              }
            />
          </Routes>
        </main>
      </BrowserRouter>
    );
  }
  
  export default App;
