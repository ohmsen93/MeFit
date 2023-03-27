import './App.css';
// Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";
// Bootstrap Bundle JS
import "bootstrap/dist/js/bootstrap.bundle.min";

import { BrowserRouter, Routes, Route } from "react-router-dom"
import KeycloakRoute from './Hoc/keycloakRoutes';
import Dashboard from "./Views/Dashboard"
import Login from "./Views/Login"
import GoalCreation from './Views/GoalCreation';
import Navbar from './Components/navbar/Navbar';
import Contributor from './Views/Contributor';
import UserProfile from './Views/UserProfile';
import { useAuth } from './Components/context/AuthenticateContext';
import { Roles } from './Components/roles/Roles';
import GoalsOverview from './Views/GoalsOverview';
import ProgramsOverview from './Views/ProgramsOverview';
import WorkoutsOverview from './Views/WorkoutsOverview';
import ExercisesOverview from './Views/ExercisesOverview';

function App() {

  const { auth } = useAuth();

  return (
    <BrowserRouter>
      {auth && (
        <Navbar />
      )}
      <main className="container">
        <Routes>
          {auth
            ? <>
              <Route path="/dashboard" element={<KeycloakRoute role={Roles.Regular}> <Dashboard /> </KeycloakRoute>} />
              <Route path="/contributor" element={<KeycloakRoute role={Roles.Contributor}> <Contributor /> </KeycloakRoute>} />
              <Route path="/profile" element={<KeycloakRoute role={Roles.Regular}> <UserProfile /> </KeycloakRoute>} />
              <Route path="/goals" element={<KeycloakRoute role={Roles.Regular}> <GoalsOverview /> </KeycloakRoute>} />
              <Route path="/goals/new" element={<KeycloakRoute role={Roles.Regular}><GoalCreation /> </KeycloakRoute>} />
              <Route path="/programs" element={<KeycloakRoute role={Roles.Regular}><ProgramsOverview contributor={useAuth().role.length > 1} /> </KeycloakRoute>} />
              <Route path="/workouts" element={<KeycloakRoute role={Roles.Regular}><WorkoutsOverview contributor={useAuth().role.length > 1} /> </KeycloakRoute>} />
              <Route path="/exercises" element={<KeycloakRoute role={Roles.Regular}><ExercisesOverview contributor={useAuth().role.length > 1} /> </KeycloakRoute>} />
            </>
            :
            <Route path="/" element={<Login />}
            />
          }
        </Routes>
      </main>
    </BrowserRouter>
  );
}

export default App;
