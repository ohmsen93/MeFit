import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserFitnessModal(props) {

    const [modalData, setModalData] = useState({
        key: UserFitnessModal.name,
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
                <Modal.Title>Personal Fitness Information</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Weight</Form.Label>
                        <Form.Control
                            required
                            type="number"
                            placeholder="weight in kg">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Height</Form.Label>
                        <Form.Control
                            required
                            type="number"
                            placeholder="height in cm">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>MedicalCondition</Form.Label>
                        <Form.Control as="textarea" rows={4}
                            required
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Disabilities</Form.Label>
                        <Form.Control as="textarea" rows={4}
                            required
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                {props.onFirstLogin
                    ? <Button variant="primary" onClick={e => handleNext(e)}> Save Changes </Button>
                    : <Button variant="primary" onClick={props.onModalClose}> Close </Button>
                }
            </Modal.Footer>
        </Modal>
    );
}

export default UserFitnessModal