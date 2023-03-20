import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserFitnessModal(props) {

    const [show, setShow] = useState(props.requestOpen)
    const [modalData, setModalData] = useState({
        key: UserFitnessModal.name,
        card: "UserFitnessCard",
        weight: 0.0,
        height: 0.0,
        medicalCondition: "",
        disabilities: ""
    })

    function handleChange(event) {
        const key = event.target.name;
        const value = event.target.value;
        setModalData({ ...modalData, [key]: value })
    }

    function handleClose() {
        setShow(false)
        props.onModalClose();
    }

    function handleSave(event) {
        props.onSave(event, modalData);
        handleClose();
    }

    return (
        <Modal show={show} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
                <Modal.Title>Personal Fitness Information</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Weight</Form.Label>
                        <Form.Control
                            name="weight"
                            required
                            type="number"
                            onChange={handleChange}
                            placeholder="weight in kg">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Height</Form.Label>
                        <Form.Control
                            name="height"
                            required
                            type="number"
                            onChange={handleChange}
                            placeholder="height in cm">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>MedicalCondition</Form.Label>
                        <Form.Control as="textarea" rows={4}
                            name="medicalCondition"
                            required
                            type="text"
                            onChange={handleChange}
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Disabilities</Form.Label>
                        <Form.Control as="textarea" rows={4}
                            name="disabilities"
                            required
                            type="text"
                            onChange={handleChange}
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                {props.onFirstLogin
                    ? <Button variant="primary" onClick={(e => handleSave(e))}> Save Changes </Button>
                    : <>
                        <Button variant="primary" onClick={(e => handleClose())}> Close </Button>
                        <Button variant="primary" onClick={(e => handleSave(e))}> Save Changes </Button>
                    </>
                }
            </Modal.Footer>
        </Modal>
    );
}

export default UserFitnessModal