import React, { useEffect, useState } from 'react';
import UserAddressModal from '../Modals/UserAddressModal';
import UserFitnessModal from '../Modals/UserFitnessModal';
import UserInformationModal from '../Modals/UserInformationModal';
import UserInformationCard from '../Cards/UserInformationCard'
import UserAddressCard from '../Cards/UserAddressCard';
import UserFitnessCard from '../Cards/UserFitnessCard';
import keycloak from '../../keycloak';
import { fetchUserById, postUser } from '../API/UserApi';


async function OnIntialProfileLoad(setLoading, setUserData, setFirstLogin) {

    let data = await fetchUserById(keycloak.tokenParsed.user_Id);

    if (!data?.ok) {
        const tempData = {
            firstName: keycloak.tokenParsed?.firstName || "",
            lastName: keycloak.tokenParsed?.lastName || "",
            email: keycloak.tokenParsed?.email || "",
        }
        data = tempData;
    }

    if (data == null || data == undefined) {
        setLoading(true);
    }
    else {
        setUserData(data);
        setFirstLogin(data?.userData?.firstLogin || true);
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
                setFirstLogin);
        }
        if (userData == null || userData == undefined) {
            GetUserData();
        }
    }, [])



    if (loading) {

    }
    else {
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

    function handlePayload(key, payload) {
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
        console.log(data);
        setCardRequiredUpdate(data);
        if (isfirstlogin) {
            handlePayload(data.key, data);
            handleNewUser(userPayLoad);
        }
    }

    async function handleNewUser(payload) {
        setFirstLogin(false);
        console.log(payload);
        const data = await postUser(payload, isfirstlogin);
        setUserData(data);
    }

    function handlePatchUser(id, payload) {
        console.log(id, payload);
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

    function handleAfterCardUpdate() {
        if (cardUpdateRequired != null || cardUpdateRequired != undefined) { setCardRequiredUpdate(null); }
    }

    function handleNextModal(event, data, key) {
        setCardRequiredUpdate(data);
        handlePayload(key, data);
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
                    <h1>Test</h1>
                </>
                :
                <>
                    {isfirstlogin
                        ?
                        <>
                            {currentmodal}
                            <UserInformationCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserProfileCard")} afterUpdate={handleAfterCardUpdate} />
                            <UserAddressCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserAddressCard")} afterUpdate={handleAfterCardUpdate} />
                            <UserFitnessCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserFitnessCard")} afterUpdate={handleAfterCardUpdate} />
                        </>
                        :
                        <>
                            {currentmodal}
                            <UserInformationCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserProfileCard")} afterUpdate={handleAfterCardUpdate} />
                            <UserAddressCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserAddressCard")} afterUpdate={handleAfterCardUpdate} />
                            <UserFitnessCard onModalOpen={handleOpenModal} userData={userData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserFitnessCard")} afterUpdate={handleAfterCardUpdate} />
                        </>
                    }
                </>
            }

        </>
    );
}

export default UserProfileForm;
