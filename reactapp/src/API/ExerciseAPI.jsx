import keycloak from "../keycloak"

// FETCH
export const fetchExercises = async () => {
    console.log(process.env.REACT_APP_API_URL + "/exercises")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/exercises", {
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Authorization': 'Bearer ' + keycloak.token
            }
        })
            .then(response => response.json())
            .then(results => {
                return results
            })
        return request
    } catch (error) {
        console.log(error)
    }
}