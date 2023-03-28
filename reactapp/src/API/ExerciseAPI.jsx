import keycloak from "../keycloak"

// FETCH
export const fetchExercises = async () => {
    // console.log(process.env.REACT_APP_API_URL + "/exercises")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/exercises", {
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
export const postExercise = async (exercise) => {
    // console.log(process.env.REACT_APP_API_URL + "/exercises")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/exercises", {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(exercise)
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
export const patchExercise = async (exerciseId, newExercise) => {
    // console.log(process.env.REACT_APP_API_URL + "/exercises")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/exercises/" + exerciseId, {
            method: 'PATCH',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(newExercise)
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
export const patchExerciseMusclegroups = async (exerciseId, newMusclegroups) => {
    // console.log(process.env.REACT_APP_API_URL + "/exercises")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/exercises/" + exerciseId + "/musclegroups", {
            method: 'PATCH',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(newMusclegroups)
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
export const patchExerciseSets = async (exerciseId, newSets) => {
    // console.log(process.env.REACT_APP_API_URL + "/exercises")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/exercises/" + exerciseId + "/sets", {
            method: 'PATCH',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(newSets)
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