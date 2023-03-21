import keycloak from "../keycloak";

function Dashboard (){
  return (
  <div>
          <h1>DashBoard</h1>
          {console.log(keycloak.token)}
    </div>
  );
}

export default Dashboard;