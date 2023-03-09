import keycloak from "../../keycloak";
import { ROLES } from "../../const/roles";
import { isAuthenticated } from "../Security/Authentication";

const isAuthorized = isAuthenticated();
const paths = [
  { name: "Dashboard", path: "Dashboard" },
  { name: "Profile", path: "Profile" },
  { name: "Contributor", path: "Contributor" }
]

function Navbar() {
  return (
    <nav className="navbar navbar-expand-sm navbar-dark bg-dark">
      <a className="navbar-brand" href="#">Expand at sm</a>
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
            else if (keycloak.hasRealmRole(ROLES.Contributor) | keycloak.hasRealmRole(ROLES.Administrator)){
              return (
                <li key={index} className="nav-item">
                  <a className="nav-link" href={newPath.path}>{newPath.name}</a>
                </li>
              );
            }
          })}
         <button onClick={() => keycloak.logout()}>Logout</button>
        </ul>
      </div>
    </nav>
  );
}
export default Navbar;
