import keycloak from "../../keycloak";
import { useAuth } from "../context/AuthenticateContext";
import { Roles } from "../roles/Roles";

const logo = require("./../../Images/icons8-user-100.png");

const paths = [
  { name: "Dashboard", path: "/Dashboard" },
  { name: "Programs", path: "/programs" },
  { name: "Workouts", path: "/workouts" },
  { name: "Exercises", path: "/exercises" },
  { name: "Profile", path: "/Profile" },
  { name: "Contributor", path: "/Contributor" }
]

const hasRole = (roleCollection, toMatch) => {
  for (let index = 0; index < roleCollection.length; index++) {
    if (roleCollection[index].match(toMatch)) return true;
    else return false;
  }
}

function Navbar() {

  const { auth, role, setAuth } = useAuth();
  return (
    <nav className="navbar navbar-expand-sm navbar-dark bg-dark">
      <a className="navbar-brand" >MeFit</a>
      <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExample03" aria-controls="navbarsExample03" aria-expanded="false" aria-label="Toggle navigation">
        <span className="navbar-toggler-icon"></span>
      </button>
      <div className="collapse navbar-collapse" id="navbarsExample03">
        <ul className="navbar-nav mr-auto">
          {paths.map((newPath, index) => {
            if ((!hasRole(role,Roles.Contributor) || !hasRole(role,Roles.Admin)) && !newPath.name.match("Contributor")) {
              return (
                <li key={index} className="nav-item">
                  <a className="nav-link" href={newPath.path}>{newPath.name}</a>
                </li>
              );
            }
            else if (hasRole(role,Roles.Contributor) || hasRole(role,Roles.Admin)) {
              return (
                <li key={index} className="nav-item">
                  <a className="nav-link" href={newPath.path}>{newPath.name}</a>
                </li>
              );
            }
          })}
          <a className="navbar-brand" href="Profile">
            <img src={logo} width="30" height="24"></img>
          </a>
          <button onClick={() => keycloak.logout(setAuth(!auth))}>Logout</button>
        </ul>
      </div>
    </nav>
  );
}
export default Navbar;
