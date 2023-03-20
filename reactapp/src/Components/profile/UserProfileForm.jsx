import React, { useState } from 'react';
import UserAddressModal from '../Modals/UserAddressModal';
import UserFitnessModal from '../Modals/UserFitnessModal';
import UserInformationModal from '../Modals/UserInformationModal';
import UserInformationCard from '../Cards/UserInformationCard'
import UserAddressCard from '../Cards/UserAddressCard';
import UserFitnessCard from '../Cards/UserFitnessCard';


const DefaultModalState = (show, isfirstlogin, handleCloseModal, handleNextModal, handleSaveModal) => {
    const defaultState = isfirstlogin && (
        <UserInformationModal
            requestOpen={show}
            onModalClose={handleCloseModal}
            onHandleNext={handleNextModal}
            onSave={handleSaveModal}
            onFirstLogin={isfirstlogin}
        />
    )

    return defaultState
}

function UserProfileForm() {

    const [show, setShow] = useState(true)
    const [isfirstlogin] = useState(true)
    const [currentmodal, setNewModal] = useState(DefaultModalState(show, isfirstlogin, handleCloseModal, handleNextModal, handleSaveModal))
    const [cardUpdateRequired, setCardRequiredUpdate] = useState(null)

    const profileData = []

    function handleOpenModal(key) {
        switch (key) {
            case "UserInformationModal":
                setNewModal(
                    <UserInformationModal
                        requestOpen={show}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                    />
                )
                break;
            case "UserAddressModal":
                setNewModal(
                    <UserAddressModal
                        requestOpen={show}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                    />
                )
                break;
            case "UserFitnessModal":
                setNewModal(
                    <UserFitnessModal
                        requestOpen={show}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                    />
                )
                break;
                default:
                    break;
        }
    }

    function handleCloseModal() {
        setNewModal(null);
    }

    function handleSaveModal(event, data) {
        setCardRequiredUpdate(data);
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
        if (cardUpdateRequired != null || cardUpdateRequired != undefined){setCardRequiredUpdate(null);}    
    }

    function handleNextModal(event, data, key) {
        handleSaveModal(event, data);
        switch (key) {
            case "UserInformationModal":
                setNewModal(
                    <UserAddressModal
                        requestOpen={show}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                    />
                )
                break;
            case "UserAddressModal":
                setNewModal(
                    <UserFitnessModal
                        requestOpen={show}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onSave={handleSaveModal}
                        onFirstLogin={isfirstlogin}
                    />
                )
                break;
                default:
                    break;
        }

    }

    return (
        <>
            {isfirstlogin
                ?
                <>
                    {currentmodal}
                    <UserInformationCard onModalOpen={handleOpenModal} userData={profileData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserProfileCard")} afterUpdate={handleAfterCardUpdate} />
                    <UserAddressCard onModalOpen={handleOpenModal} userData={profileData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserAddressCard")} afterUpdate={handleAfterCardUpdate}/>
                    <UserFitnessCard onModalOpen={handleOpenModal} userData={profileData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserFitnessCard")} afterUpdate={handleAfterCardUpdate}/>
                </>
                :
                <>
                    {currentmodal}
                    <UserInformationCard onModalOpen={handleOpenModal} userData={profileData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserProfileCard")} afterUpdate={handleAfterCardUpdate} />
                    <UserAddressCard onModalOpen={handleOpenModal} userData={profileData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserAddressCard")} afterUpdate={handleAfterCardUpdate}/>
                    <UserFitnessCard onModalOpen={handleOpenModal} userData={profileData} updateRequired={handleCardUpdate(cardUpdateRequired, "UserFitnessCard")} afterUpdate={handleAfterCardUpdate}/>
                </>
            }

        </>
    );
}

export default UserProfileForm;
