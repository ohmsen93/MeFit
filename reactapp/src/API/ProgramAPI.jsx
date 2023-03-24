import keycloak from "../keycloak"

// FETCH
export const fetchPrograms = async () => {
    console.log(process.env.REACT_APP_API_URL + "/trainingprograms")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/trainingprograms", {
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
export const postProgram = async (program) => {
    console.log(process.env.REACT_APP_API_URL + "/trainingprograms")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/trainingprograms", {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(program)
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
export const patchProgram = async (programId, newProgram) => {
    console.log(process.env.REACT_APP_API_URL + "/trainingprograms")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/trainingprograms/" + programId, {
            method: 'PATCH',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(newProgram)
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
export const patchProgramWorkouts = async (programId, newWorkouts) => {
    console.log(process.env.REACT_APP_API_URL + "/trainingprograms")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/trainingprograms/" + programId + "/workouts", {
            method: 'PATCH',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(newWorkouts)
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
export const patchProgramCategories = async (programId, newCategories) => {
    console.log(process.env.REACT_APP_API_URL + "/trainingprograms")
    try {
        const request = await fetch(process.env.REACT_APP_API_URL + "/trainingprograms/" + programId + "/categories", {
            method: 'PATCH',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify(newCategories)
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