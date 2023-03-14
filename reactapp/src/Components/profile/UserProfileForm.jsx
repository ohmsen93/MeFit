import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import UserAddressModal from '../Modals/UserAddressModal';
import UserFitnessModal from '../Modals/UserFitnessModal';
import UserInformationModal from '../Modals/UserInformation';


const UserProfileForm = () => {

    const [show, setShow] = useState(true);

    return (
        <>
            <UserInformationModal show={show} onHide={() => setShow(false)} />
        </>
    );
}

export default UserProfileForm;