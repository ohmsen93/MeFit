import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserInformationModal(props) {
    return (
        <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
                <Modal.Title>Personal Information</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>First Name</Form.Label>
                        <Form.Control
                            required
                            defaultValue={keycloak.tokenParsed?.firstName || ""}
                            type="text"
                            placeholder="first name">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Last Name</Form.Label>
                        <Form.Control
                            required
                            defaultValue={keycloak.tokenParsed?.lastName || ""}
                            type="text"
                            placeholder="last name">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Email</Form.Label>
                        <Form.Control
                            required
                            defaultValue={keycloak.tokenParsed?.email || ""}
                            type="email"
                            placeholder="example@gmail.com">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Phone Number</Form.Label>
                        <Form.Control
                            required
                            type="tel"
                            placeholder="45+11111111">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Profile picture</Form.Label>
                        <Form.Control
                            required
                            type="file"
                        >
                        </Form.Control>
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={props.onHide}>
                    Close
                </Button>
                <Button variant="primary" onClick={props.onHide}>
                    Save Changes
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

export default UserInformationModal