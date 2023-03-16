import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserInformationModal(props) {

    const [modalData, setModalData] = useState({
        key: UserInformationModal.name,
        firstName: keycloak.tokenParsed?.firstName || "",
        lastName: keycloak.tokenParsed?.lastName || "",
        email: keycloak.tokenParsed?.email || "",
        phoneNumber: "",
        profilePicture: ""
    })

    function handleChange(event) {
        const key = event.target.name;
        const value = event.target.value;
        setModalData({ ...modalData, [key]: value })
    }

    function handleClose(event) {
        props.onModalClose(event, modalData);
    }

    function handleNext(event) {
        props.onHandleNext(event, modalData, modalData.key);
    }

    return (
        <Modal show={props.isModalOpen} aria-labelledby="contained-modal-title-vcenter">
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
                            defaultValue={keycloak.tokenParsed?.firstName || ""}
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
                            defaultValue={keycloak.tokenParsed?.lastName || ""}
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
                            defaultValue={keycloak.tokenParsed?.email || ""}
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
                            onChange={handleChange}
                            placeholder="45+11111111">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Profile picture</Form.Label>
                        <Form.Control
                            name="profilePicture"
                            required
                            onChange={handleChange}
                            type="file">
                        </Form.Control>
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                {props.onFirstLogin
                    ? <Button variant="primary" onClick={e => handleNext(e)}> Next </Button>
                    : <Button variant="primary" onClick={props.onModalClose}> Close </Button>
                }
            </Modal.Footer>
        </Modal>
    );
}

export default UserInformationModal