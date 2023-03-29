import React, { useEffect, useState } from 'react';
import UserAddressModal from '../Modals/UserAddressModal';
import UserFitnessModal from '../Modals/UserFitnessModal';
import UserInformationModal from '../Modals/UserInformationModal';
import UserInformationCard from '../Cards/UserInformationCard'
import UserAddressCard from '../Cards/UserAddressCard';
import UserFitnessCard from '../Cards/UserFitnessCard';
import keycloak from '../../keycloak';
import { fetchUserById, patchAddressById, patchFitnessById, patchPersonalById, postUser } from '../API/UserApi';


async function OnIntialProfileLoad(setLoading, setUserData, setFirstLogin, userData) {

    setLoading(true);

    let data

    if (userData == null || userData == undefined) {
        data = await fetchUserById(keycloak.tokenParsed.user_Id);
        if (data == "404") {
            const tempData = {
                firstName: keycloak.tokenParsed?.firstName || "",
                lastName: keycloak.tokenParsed?.lastName || "",
                email: keycloak.tokenParsed?.email || "",
                firstLogin: true
            }
            data = tempData;
        }
    }
    else { data = userData }
   

    if (data != null || data != undefined) {
        setUserData(data);
        setFirstLogin(data?.userData?.firstLogin || data?.firstLogin);
        setLoading(false);
    }
}

function UserProfileForm() {

    const [show, setShow] = useState(true)
    const [isfirstlogin, setFirstLogin] = useState(false)
    const [currentmodal, setNewModal] = useState(null)
    const [cardUpdateRequired, setCardRequiredUpdate] = useState(null)
    const [userData, setUserData] = useState(null);
    const [loading, setLoading] = useState(true);
    const userPayLoad = {
        userData: [],
        profileData: [],
        adressData: []
    }


    useEffect(() => {
        const GetUserData = async () => {
            await OnIntialProfileLoad(
                setLoading,
                setUserData,
                setFirstLogin,
                userData
            );
        }
        if (userData == null || userData == undefined) {
            GetUserData();
        }
    }, [])



    if (!loading) {
        if (isfirstlogin && currentmodal == null) {
            handleOpenModal("UserInformationModal");
        }
    }



    function handleOpenModal(key) {
        switch (key) {
            case "UserInformationModal":
                setNewModal(
                    <UserInformationModal
                        requestOpen={true}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                        onUserData={userData}
                    />
                )
                break;
            case "UserAddressModal":
                setNewModal(
                    <UserAddressModal
                        requestOpen={true}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                        onUserData={userData}
                    />
                )
                break;
            case "UserFitnessModal":
                setNewModal(
                    <UserFitnessModal
                        requestOpen={true}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                        onUserData={userData}
                    />
                )
                break;
            default:
                break;
        }
    }

    function handleCloseModal() {
        setNewModal(null);
        setShow(false);
    }

    function handleNewUserPayload(key, payload) {
        switch (key) {
            case "UserInformationModal":
                userPayLoad.userData = payload
                break;
            case "UserAddressModal":
                userPayLoad.adressData = payload
                break;
            case "UserFitnessModal":
                userPayLoad.profileData = payload
                break;
            default:
                break;
        }
    }

    function handleSaveModal(data) {

        setCardRequiredUpdate(data);

        if (isfirstlogin) {
            handleNewUserPayload(data.key, data);
            handleNewUser(userPayLoad);
        }
        else {
            handlePatchPayload(data, userData);
        }
    }

    async function handleNewUser(payload) {
        setFirstLogin(false);
        console.log(payload);
        const data = await postUser(payload, isfirstlogin);
        setUserData(data);
    }

    async function handlePatchPayload(payload, data) {
        switch (payload.card) {
            case "UserProfileCard":
                await patchPersonalById(payload, data);
                await handleDataChange(payload, data);
                break;
            case "UserAddressCard":
                await patchAddressById(payload, data);
                await handleDataChange(payload, data);
                break;
            case "UserFitnessCard":
                await patchFitnessById(payload, data);
                await handleDataChange(payload, data);
                break;
            default:
                break;
        }

    }

    async function handleDataChange(payload, data) {
        switch (payload.card) {
            case "UserProfileCard":
                data.profileData.email = payload.email;
                data.profileData.firstname = payload.firstName;
                data.profileData.lastname = payload.lastName;
                data.profileData.phone = payload.phoneNumber;
                data.profileData.picture = payload.profilePicture;
                break;
            case "UserAddressCard":
                data.adressData.addressLine1 = payload.addressLine1;
                data.adressData.addressLine2 = payload.addressLine2;
                data.adressData.addressLine3 = payload.addressLine3;
                data.adressData.city = payload.city;
                data.adressData.country = payload.country;
                data.adressData.postalCode = payload.postalCode;
                break;
            case "UserFitnessCard":
                data.profileData.weight = payload.weight;
                data.profileData.height = payload.height;
                data.profileData.medicalCondition = payload.medicalCondition;
                data.profileData.disabilities = payload.disabilities;
                break;
            default:
                break;
        }
    }

    function handleCardUpdate(data, cardToUpdate) {
        if (cardUpdateRequired != null) {
            if (data !== undefined) {
                if (data.card != undefined && data.card.match(cardToUpdate)) {
                    return data;
                }
            }
            return null;
        }
    }

    function handleNextModal(event, data, key) {
        setCardRequiredUpdate(data);
        handleNewUserPayload(key, data);
        console.log(key);
        switch (key) {
            case "UserInformationModal":
                setNewModal(
                    <UserAddressModal
                        requestOpen={true}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                        onUserData={userData}
                    />
                )
                break;
            case "UserAddressModal":
                setNewModal(
                    <UserFitnessModal
                        requestOpen={true}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                        onUserData={userData}
                    />
                )
                break;
            default:
                break;
        }

    }

    return (
        <>
            {loading
                ?
                <>
                    <h1>Loading...</h1>
                </>
                :
                <>
                    {isfirstlogin
                        ?
                        <>
                            {currentmodal}
                            <UserInformationCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserProfileCard")} />
                            <UserAddressCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserAddressCard")} />
                            <UserFitnessCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserFitnessCard")} />
                        </>
                        :
                        <>
                            {currentmodal}
                            <UserInformationCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserProfileCard")} />
                            <UserAddressCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserAddressCard")} />
                            <UserFitnessCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserFitnessCard")} />
                        </>
                    }
                </>
            }

        </>
    );
}

export default UserProfileForm;
