import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserFitnessModal(props) {

    const [form, setForm] = useState({
        weight: props?.onUserData?.profileData?.weight,
        height: props?.onUserData?.profileData?.height,
        medicalCondition: props?.onUserData?.profileData?.medicalCondition,
        disabilities: props?.onUserData?.profileData?.disabilities
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
                key: UserFitnessModal.name,
                card: "UserFitnessCard",
                weight: props?.onUserData?.profileData?.weight || 0.0,
                height: props?.onUserData?.profileData?.height || 0.0,
                medicalCondition: props?.onUserData?.profileData?.medicalCondition || "",
                disabilities: props?.onUserData?.profileData?.disabilities || ""
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

    function validateForm() {
        const { weight, height, medicalCondition, disabilities } = form;
        const newErrors = {};

        if (!weight || weight === '') { newErrors.weight = "Please Enter Your Weight" }
        if (!height || height === '') { newErrors.height = "Please Enter Your Height" }
        if (!medicalCondition || medicalCondition === '') { newErrors.medicalCondition = "Write ? if no known issues exist" }
        if (!disabilities || disabilities.length === '') { newErrors.disabilities = "Write ? if no known issues exist" }


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
                            onChange={(e) => { handleChange(e); setField('weight', e.target.value); }}
                            placeholder="weight in kg"
                            isInvalid={!!errors.weight}>
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.weight}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Height</Form.Label>
                        <Form.Control
                            name="height"
                            required
                            type="number"
                            defaultValue={modalData?.height || ""}
                            onChange={(e) => { handleChange(e); setField('height', e.target.value); }}
                            placeholder="height in cm"
                            isInvalid={!!errors.height}>
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.height}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>MedicalCondition</Form.Label>
                        <Form.Control as="textarea" rows={4}
                            name="medicalCondition"
                            required
                            type="text"
                            defaultValue={modalData?.medicalCondition || ""}
                            onChange={(e) => { handleChange(e); setField('medicalCondition', e.target.value); }}
                            placeholder=""
                            isInvalid={!!errors.medicalCondition}>
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.medicalCondition}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Disabilities</Form.Label>
                        <Form.Control as="textarea" rows={4}
                            name="disabilities"
                            required
                            type="text"
                            defaultValue={modalData?.disabilities || ""}
                            onChange={(e) => { handleChange(e); setField('disabilities', e.target.value); }}
                            placeholder=""
                            isInvalid={!!errors.disabilities}>
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.disabilities}
                        </Form.Control.Feedback>
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