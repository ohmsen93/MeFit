import AuthProvider from "./AuthenticateContext";

const AppContext = ({ children }) => {
    return (
        <AuthProvider>
            {children}
        </AuthProvider>
    )
}

export default AppContext;