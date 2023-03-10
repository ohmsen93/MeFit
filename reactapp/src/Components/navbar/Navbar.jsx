import keycloak from "../../keycloak";
import { ROLES } from "../../const/roles";
import { useAuth } from "../context/AuthenticateContext";

const logo = require("./../../Images/icons8-user-100.png");

const paths = [
  { name: "Dashboard", path: "Dashboard" },
  { name: "Profile", path: "Profile" },
  { name: "Contributor", path: "Contributor" }
]

function Navbar() {

  const { auth, setAuth } = useAuth();

  return (
    <nav className="navbar navbar-expand-sm navbar-dark bg-dark">
      <a className="navbar-brand" >MeFit</a>
      <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExample03" aria-controls="navbarsExample03" aria-expanded="false" aria-label="Toggle navigation">
        <span className="navbar-toggler-icon"></span>
      </button>
      <div className="collapse navbar-collapse" id="navbarsExample03">
        <ul className="navbar-nav mr-auto">
          {paths.map((newPath, index) => {
            if (!keycloak.hasRealmRole(ROLES.Contributor) | !keycloak.hasRealmRole(ROLES.Administrator) && !newPath.name.match("Contributor")) {
              return (
                <li key={index} className="nav-item">
                  <a className="nav-link" href={newPath.path}>{newPath.name}</a>
                </li>
              );
            }
            else if (keycloak.hasRealmRole(ROLES.Contributor) | keycloak.hasRealmRole(ROLES.Administrator)) {
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
