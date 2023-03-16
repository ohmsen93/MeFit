import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserAddressModal(props) {

    const [modalData, setModalData] = useState({
        key: UserAddressModal.name,
        firstName: keycloak.tokenParsed?.firstName || "",
        lastName: keycloak.tokenParsed?.lastName || "",
        email: keycloak.tokenParsed?.email || "",
        phoneNumber: undefined,
        profilePicture: undefined
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
                <Modal.Title>Personal Address Information</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address</Form.Label>
                        <Form.Control
                            required
                            type="text"
                            placeholder="required">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 1</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 2</Form.Label>
                        <Form.Control
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Postal Code</Form.Label>
                        <Form.Control
                            required
                            type="number"
                            placeholder="Example : 8260 would be Viby j">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>City</Form.Label>
                        <Form.Control
                            required
                            type="text"
                            placeholder="Example Viby j">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Country</Form.Label>
                        <Form.Control
                            required
                            type="text"
                            placeholder="Example Denmark">
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

export default UserAddressModal