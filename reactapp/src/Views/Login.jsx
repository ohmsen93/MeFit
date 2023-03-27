import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../Components/context/AuthenticateContext";
import LoginForm from "../Components/Login/LoginForm";
import RegisterForm from "../Components/register/RegisterForm";

function Login() {

  const { auth } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (auth) {
      { navigate("dashboard") }
    }
  }, [])

  return (
    <>
      <LoginForm />
      <RegisterForm />
    </>
  )
}

export default Login;