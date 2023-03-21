import keycloak from "../../keycloak";

const fetchUserProfileById = async (id) => {
    try {
        const options = {
            method: 'GET',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            }
        }
        const request = await fetch(`https://localhost:7101/${id}`, options)
            .then(response => response.json())
            .then(results => {
                return results
            })
        return request;
    } catch (error) {
        console.log(error);
    }
}

const fetchUserAddressById = async (id) => {
    try {
        const options = {
            method: 'GET',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            }
        }
        const request = await fetch(`https://localhost:7101/api/addresses/${id}`, options)
            .then(response => response.json())
            .then(results => {
                return results
            })
        return request;
    } catch (error) {

    }
}

export const fetchUserById = async (id) => {
    try {
        const options = {
            method: 'GET',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            }
        }
        let responses;
        const user = await fetch(`https://localhost:7101/api/users/${id}`, options)
            .then(async (response) => {
                if (!response.ok) {
                    responses = response;
                }
                else {
                    response.json()

                    const profile = await fetchUserProfileById(user.userProfiles.$values[0]);
                    const address = await fetchUserAddressById(profile.fkAddressId);

                    const data = {
                        userData: user,
                        profileData: profile,
                        adressData: address
                    }
                    console.log(data);
                    return data;
                }
            });
        return responses;
    } catch (error) {
        console.log(error);
    }
}

export const patchUserById = async (id, payload) => {
    try {
        const patchOptions = {
            method: 'PATCH',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify({
                $id: payload.userData.$id,
                firstLogin: payload.userData.firstLogin,
                username: payload.userData.username
            })
        }

        await fetch(`https://localhost:7101/api/users/${id}`, patchOptions);

    } catch (error) {
        console.log(error);
    }
}

const postAddress = async (payload) => {
    try {
        const postOptions = {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify({
                addressLine1: payload.adressData.address,
                addressLine2: payload.adressData.addressSecond,
                addressLine3: payload.adressData.addressThird,
                city: payload.adressData.city,
                country: payload.adressData.country,
                postalCode: payload.adressData.postalCode,
            })
        }

        const address = await fetch('https://localhost:7101/api/addresses', postOptions)
        .then(response => response.json());

        const data = {
            adressData: address
        }
        return data;

    } catch (error) {
        console.log(error)
    }
}

const postProfile = async (payload, address) => {
    console.log(payload);
    console.log(address);
    try {
        const postOptions = {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify({
                fkUserId: keycloak.tokenParsed.user_Id,
                fkAddressId: address.id,
                weight: payload.profileData.weight,
                height: payload.profileData.height,
                medicalCondition: payload.profileData.medicalCondition,
                disabilities: payload.profileData.disabilities,
                firstname: payload.userData.firstName,
                lastname: payload.userData.lastName,
                phone: parseInt(payload.userData.phoneNumber),
                picture: payload.userData.profilePicture,
                email: payload.userData.email,
            })
        }

        const profile = await fetch('https://localhost:7101/api/userprofiles')
        .then(response => response.json());
        
        console.log(profile);
        
        const data = {
            profileData: profile
        }
        
        return data;

    } catch (error) {
        console.log(error)
    }
}

export const postUser = async (payload, firstlogin) => {
    try {

        const address = await postAddress(payload);
        const profile = await postProfile(payload, address.adressData);
        
        const postOptions = {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + keycloak.token
            },
            body: JSON.stringify({
                id: profile.profileData.fkUserId,
                username: profile.profileData.email,
                firstLogin: firstlogin,
            })
        }

        const user = await fetch('https://localhost:7101/api/users', postOptions)
        .then(response => response.json());

        const data = {
            userData: user,
            profileData: profile,
            adressData: address
        }

        console.log(data);
        return data;
    } catch (error) {
        console.log(error)
    }
}

