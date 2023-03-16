import keycloak from "../keycloak"

// FETCH
export const fetchGoals = async () => {
    console.log(process.env.REACT_APP_API_URL + "/goals")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/goals", {
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

// POST
export const postGoal = async (goal) => {
    console.log(process.env.REACT_APP_API_URL + "/goals")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/goals", {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(goal)
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