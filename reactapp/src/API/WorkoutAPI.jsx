import keycloak from "../keycloak"

// FETCH
export const fetchWorkouts = async () => {
    console.log(process.env.REACT_APP_API_URL + "/workouts")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/workouts", {
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
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
export const postWorkout = async (workout) => {
    console.log(process.env.REACT_APP_API_URL + "/workouts")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/workouts", {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(workout)
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

// PATCH
export const patchWorkout = async (workoutId, newWorkout) => {
    console.log(process.env.REACT_APP_API_URL + "/workouts")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/workouts/" + workoutId, {
            method: 'PATCH',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(newWorkout)
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