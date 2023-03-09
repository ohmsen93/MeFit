import './App.css';
// Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";
// Bootstrap Bundle JS
import "bootstrap/dist/js/bootstrap.bundle.min";

import { BrowserRouter, Routes, Route } from "react-router-dom"
import KeycloakRoute from './Hoc/keycloakRoutes';
import { ROLES } from "./const/roles"
import Dashboard from "./Views/Dashboard"
import Login from "./Views/Login"
import Navbar from './Components/navbar/Navbar';
import keycloak from './keycloak';

function App() {
    return (
      <BrowserRouter>
      {keycloak.authenticated && (
        <Navbar />
      )}
        <main className="container">
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/dashboard" element={<KeycloakRoute role={ ROLES.Regular }> <Dashboard /> </KeycloakRoute>}/>
            <Route path="/contributor" element={<KeycloakRoute role={ ROLES.Contributor | ROLES.Administrator }> <Dashboard /> </KeycloakRoute>}/>
          </Routes>
        </main>
      </BrowserRouter>
    );
  }
  
  export default App;
