import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import UserAddressModal from '../Modals/UserAddressModal';
import UserFitnessModal from '../Modals/UserFitnessModal';
import UserInformationModal from '../Modals/UserInformation';


const UserProfileForm = () => {

    const [show, setShow] = useState(true)
    const [isfirstlogin] = useState(true)
    const [currentmodal, setNewModal] = useState
        (
            isfirstlogin && (
                <UserInformationModal
                    isModalOpen={show}
                    onModalClose={handleCloseModal}
                    onHandleNext={handleNextModal}
                    onFirstLogin={isfirstlogin}
                />
            )
        );

    const intialProfileData = []

    function handleCloseModal(event, data) {
        console.log(event, data);
        intialProfileData.push(data);
        console.log(intialProfileData);
        setShow(false);
    }

    function handleNextModal(event, data, key) {
        handleCloseModal(event, data);

        switch (key) {
            case "UserInformationModal":
                setNewModal(
                    <UserAddressModal
                        isModalOpen={show}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onFirstLogin={isfirstlogin}
                    />
                )
                break;
            case "UserAddressModal":
                setNewModal(
                    <UserFitnessModal
                        isModalOpen={show}
                        onModalClose={handleCloseModal}
                        onHandleNext={handleNextModal}
                        onFirstLogin={isfirstlogin}
                    />
                )
                break;
        }

    }

    return (
        <>
            {currentmodal}
        </>
    );
}

export default UserProfileForm;
