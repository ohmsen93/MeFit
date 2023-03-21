import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';


function UserInformationModal(props) {

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
                phoneNumber: "",
                profilePicture: ""
            }
            return modalData;
        }
        else {
            // check database for data to populate the modal
            modalData = {
                key: UserInformationModal.name,
                id: props.onUserData.$id,
                card: "UserProfileCard",
                firstName: props.onUserData.profileData.firstname || "",
                lastName: props.onUserData.profileData.lastname || "",
                email: props.onUserData.profileData.email || "",
                phoneNumber: props.onUserData.profileData.phone || "",
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

    function handleSave() {
        props.onSave(modalData);
        handleClose();
    }

    return (
        <Modal show={show} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
                <Modal.Title>Personal Information</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>First Name</Form.Label>
                        <Form.Control
                            name="firstName"
                            required
                            defaultValue={modalData?.firstName || ""}
                            type="text"
                            onChange={handleChange}
                            placeholder="first name">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Last Name</Form.Label>
                        <Form.Control
                            name="lastName"
                            required
                            defaultValue={modalData?.lastName || ""}
                            type="text"
                            onChange={handleChange}
                            placeholder="last name">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Email</Form.Label>
                        <Form.Control
                            name="email"
                            required
                            defaultValue={modalData?.email || ""}
                            type="email"
                            onChange={handleChange}
                            placeholder="example@gmail.com">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Phone Number</Form.Label>
                        <Form.Control
                            name="phoneNumber"
                            required
                            type="tel"
                            defaultValue={modalData?.phoneNumber || ""}
                            onChange={handleChange}
                            placeholder="45+11111111">
                        </Form.Control>
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
                        <Button variant="primary" onClick={(e => handleSave())}> Save Changes </Button>
                    </>

                }
            </Modal.Footer>
        </Modal>
    );
}

export default UserInformationModal