import { Link } from "react-router-dom";
import keycloak from "../../keycloak";

function Navbar() {

  return (
    <nav>
      <div className="container">
        <div className="navbar">
          {keycloak.authenticated && (
            <ul>
              <li>
                <button onClick={() => keycloak.logout()}>Logout</button>
              </li>
            </ul>
          )}
        </div>
      </div>
    </nav>
  );
}
export default Navbar;
