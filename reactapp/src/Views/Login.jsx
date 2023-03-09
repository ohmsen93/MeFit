import keycloak from "../keycloak";


function Login() {
  return (
    <div>
      <h1>Start Page</h1>

      <section className="actions">
        {!keycloak.authenticated && (
          <button onClick={() => keycloak.login()}>Login</button>
        )}
        {keycloak.authenticated && (
          <button onClick={() => keycloak.logout()}>Logout</button>
        )}
      </section>

    </div>
  );
}

export default Login;