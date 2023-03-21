import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserFitnessModal(props) {


    function defaultDataHandler() {
        // if it's the first time user is prompted to enter information available keycloak data will be used
        let modalData;
        if (props.onFirstLogin) {
            modalData = {
                key: UserFitnessModal.name,
                card: "UserFitnessCard",
                weight: 0.0,
                height: 0.0,
                medicalCondition: "",
                disabilities: ""
            }
            return modalData;
        }
        else {
            // check database for data to populate the modal
            modalData = {
                key: UserFitnessModal.name,
                card: "UserFitnessCard",
                weight: props.onUserData.profileData.weight || 0.0,
                height: props.onUserData.profileData.height || 0.0,
                medicalCondition: props.onUserData.profileData.medicalCondition || "",
                disabilities: props.onUserData.profileData.disabilities || ""
            }
            return modalData
        }

    }

    const [show, setShow] = useState(props.requestOpen)
    const [modalData, setModalData] = useState(defaultDataHandler())

    function handleChange(event) {
        const key = event.target.name;
        const value = event.target.value;
        setModalData({ ...modalData, [key]: value })
    }

    function handleClose() {
        setShow(false)
        props.onModalClose();
    }

    function handleSave() {
        props.onSave(modalData);
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
                            defaultValue={modalData?.weight || ""}
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
                            defaultValue={modalData?.height || ""}
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
                            defaultValue={modalData?.medicalCondition || ""}
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
                            defaultValue={modalData?.disabilities || ""}
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
                        <Button variant="primary" onClick={(e => handleSave())}> Save Changes </Button>
                    </>
                }
            </Modal.Footer>
        </Modal>
    );
}

export default UserFitnessModal