import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';


function UserInformationModal(props) {

    const [form, setForm] = useState({
        firstName: props?.onUserData?.profileData?.firstname || keycloak.tokenParsed?.firstName,
        lastName:  props?.onUserData?.profileData?.lastName || keycloak.tokenParsed?.lastName,
        email:  props?.onUserData?.profileData?.email || keycloak.tokenParsed?.email,
        phoneNumber:  props?.onUserData?.profileData?.phone
    });

    const [errors, setErrors] = useState({});

    const setField = (field, value) => {
        setForm({
            ...form,
            [field]: value
        })

        if (!!errors[field]) {
            setErrors({
                ...errors,
                [field]: null
            })
        }
    }

    function defaultDataHandler() {
        // if it's the first time user is prompted to enter information available keycloak data will be used
        let modalData;
        if (props.onFirstLogin) {
            modalData = {
                key: UserInformationModal.name,
                card: "UserProfileCard",
                firstName: keycloak.tokenParsed?.firstName || "",
                lastName: keycloak.tokenParsed?.lastName || "",
                email: keycloak.tokenParsed?.email || "",
                phoneNumber: props?.onUserData?.profileData?.phone || 0,
                profilePicture: props?.onUserData?.profileData?.picture || ""
            }
            return modalData;
        }
        else {
            // check database for data to populate the modal
            modalData = {
                key: UserInformationModal.name,
                id: props?.onUserData?.userData?.id || "",
                card: "UserProfileCard",
                firstName: props.onUserData.profileData.firstname || "",
                lastName: props.onUserData.profileData.lastname || "",
                email: props.onUserData.profileData.email || "",
                phoneNumber: props.onUserData.profileData.phone || 0,
                profilePicture: props.onUserData.profileData.picture || ""
            }
            return modalData
        }

    }

    const [show, setShow] = useState(props.requestOpen)

    const [modalData, setModalData] = useState(defaultDataHandler());

    function handleChange(event) {
        const key = event.target.name;
        const value = event.target.value;
        setModalData({ ...modalData, [key]: value })
    }

    function handleClose() {
        setShow(false)
        props.onModalClose();
    }

    function handleNext(event) {
        props.onHandleNext(event, modalData, modalData.key);
    }
   
    function validateForm() {
        const { firstName, lastName, email, phoneNumber } = form;
        const newErrors = {};

        if (!firstName || firstName === '') { newErrors.firstName = "Please Enter Your First Name" }
        if (!lastName || lastName === '') { newErrors.lastName = "Please Enter Your Last Name" }
        if (!email || email === '') { newErrors.email = "Please Enter Your Email" }
        if (!phoneNumber || phoneNumber.length === 0) { newErrors.phoneNumber = "Please Enter Your Phone Number" }
        else if (!phoneNumber || phoneNumber.length < 8) { newErrors.phoneNumber = "Please Enter A 8 Digit Phone Number" }

        return newErrors;
    }

    function handleSave(event) {
        event.preventDefault();

        const formErrors = validateForm()
        if (Object.keys(formErrors).length > 0) {
            setErrors(formErrors);
        } else {
            props.onSave(modalData);
            handleClose();
        }

    }

    return (
        <Modal show={show} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header>
                <Modal.Title>Personal Information</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3">
                        <Form.Label>First Name</Form.Label>
                        <Form.Control
                            name="firstName"
                            defaultValue={modalData?.firstName || ""}
                            required
                            type="text"
                            onChange={(e) => { handleChange(e); setField('firstName', e.target.value); }}
                            placeholder="first name"
                            isInvalid={!!errors.firstName}
                        >
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.firstName}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Last Name</Form.Label>
                        <Form.Control
                            name="lastName"
                            required
                            defaultValue={modalData?.lastName || ""}
                            type="text"
                            onChange={(e) => { handleChange(e); setField('lastName', e.target.value); }}
                            placeholder="last name"
                            isInvalid={!!errors.lastName}
                        >
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.lastName}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Email</Form.Label>
                        <Form.Control
                            name="email"
                            required
                            defaultValue={modalData?.email || ""}
                            type="email"
                            onChange={(e) => { handleChange(e); setField('email', e.target.value); }}
                            placeholder="example@gmail.com"
                            isInvalid={!!errors.email}
                        >
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.email}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Phone Number</Form.Label>
                        <Form.Control
                            name="phoneNumber"
                            required
                            type="tel"
                            defaultValue={modalData?.phoneNumber || ""}
                            onChange={(e) => { handleChange(e); setField('phoneNumber', e.target.value); }}
                            placeholder="45+11111111"
                            isInvalid={!!errors.phoneNumber}
                        >
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.phoneNumber}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Profile picture</Form.Label>
                        <Form.Control
                            name="profilePicture"
                            required
                            defaultValue={modalData?.profilePicture || ""}
                            onChange={handleChange}
                            type="file">
                        </Form.Control>
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                {props.onFirstLogin
                    ? <Button variant="primary" onClick={e => handleNext(e)}> Next </Button>
                    : <>
                        <Button variant="primary" onClick={(e => handleClose())}> Close </Button>
                        <Button variant="primary" onClick={(e => handleSave(e))}> Save Changes </Button>
                    </>

                }
            </Modal.Footer>
        </Modal>
    );
}

export default UserInformationModal