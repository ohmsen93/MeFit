import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom"
// import KeycloakRoute from './Hoc/keycloakRoutes';
// import { ROLES } from "./const/roles"
import Dashboard from "./Views/Dashboard"
import Login from "./Views/Login"
import GoalCreation from './Views/GoalCreation';

function App() {
    return (
      <BrowserRouter>
        <main className="container">
          <Routes>
            <Route path='/' element={<Login />} />
            <Route path='/dashboard' element={<Dashboard />} />
            <Route path='/goals/new' element={<GoalCreation />} />
            {/* <Route
              path="/dashboard"
              element={
                <KeycloakRoute role={ ROLES.Regular }>
                  <Dashboard />
                </KeycloakRoute>
              }
            /> */}
          </Routes>
        </main>
      </BrowserRouter>
    );
  }

export default App;
