import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserAddressModal(props) {

    const [form, setForm] = useState({
        addressLine1: props?.onUserData?.adressData?.addressLine1 || "",
        postalCode: props?.onUserData?.adressData?.postalCode || "",
        city: props?.onUserData?.adressData?.city || "",
        country: props?.onUserData?.adressData?.country || ""
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
                key: 'UserAddressModal',
                card: "UserAddressCard",
                addressLine1: props?.onUserData?.adressData?.addressLine1 || "",
                addressLine2: props?.onUserData?.adressData?.addressLine2 || "",
                addressLine3: props?.onUserData?.adressData?.addressLine3 || "",
                postalCode: props?.onUserData?.adressData?.postalCode || "",
                city: props?.onUserData?.adressData?.city || "",
                country: props?.onUserData?.adressData?.country || ""
            }
            return modalData;
        }
        else {
            // check database for data to populate the modal
            modalData = {
                key: 'UserAddressModal',
                card: "UserAddressCard",
                addressLine1: props.onUserData.adressData.addressLine1 || "",
                addressLine2: props.onUserData.adressData.addressLine2 || "",
                addressLine3: props.onUserData.adressData.addressLine3 || "",
                postalCode: props.onUserData.adressData.postalCode || "",
                city: props.onUserData.adressData.city || "",
                country: props.onUserData.adressData.country || ""
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
        event.preventDefault();
        const formErrors = validateForm()
        if (Object.keys(formErrors).length > 0) {
            setErrors(formErrors);
        } else {
            props.onHandleNext(event, modalData, modalData.key);
        }
    }

    function validateForm() {
        const { addressLine1, postalCode, city, country } = form;
        const newErrors = {};

        if (!addressLine1 || addressLine1 === '') { newErrors.addressLine1 = "Please Enter Your Address" }
        if (!postalCode || postalCode === 0) { newErrors.postalCode = "Please Enter Your Postal Code" }
        if (!city || city === '') { newErrors.city = "Please Enter Your City" }
        if (!country || country.length === '') { newErrors.country = "Please Enter Your Country" }
        if (!postalCode || postalCode.length < 4) { newErrors.postalCode = "Please Enter A Four Digit Postal Code" }


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
                <Modal.Title>Personal Address Information</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address</Form.Label>
                        <Form.Control
                            name="addressLine1"
                            required
                            type="text"
                            defaultValue={modalData?.addressLine1 || ""}
                            onChange={(e) => { handleChange(e); setField('addressLine1', e.target.value); }}
                            placeholder="required"
                            isInvalid={!!errors.addressLine1}>
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.addressLine1}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 1</Form.Label>
                        <Form.Control
                            name="addressLine2"
                            type="text"
                            defaultValue={modalData?.addressLine2 || ""}
                            onChange={(e) => { handleChange(e); }}
                            placeholder=""
                        >
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 2</Form.Label>
                        <Form.Control
                            name="addressLine3"
                            type="text"
                            defaultValue={modalData?.addressLine3 || ""}
                            onChange={(e) => { handleChange(e); }}
                            placeholder=""
                        >
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Postal Code</Form.Label>
                        <Form.Control
                            name="postalCode"
                            required
                            type="number"
                            defaultValue={modalData?.postalCode || ""}
                            onChange={(e) => { handleChange(e); setField('postalCode', e.target.value); }}
                            placeholder="Example : 8260 would be Viby j"
                            isInvalid={!!errors.postalCode}>
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.postalCode}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>City</Form.Label>
                        <Form.Control
                            name="city"
                            required
                            type="text"
                            defaultValue={modalData?.city || ""}
                            onChange={(e) => { handleChange(e); setField('city', e.target.value); }}
                            placeholder="Example Viby j"
                            isInvalid={!!errors.city}>
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.city}
                        </Form.Control.Feedback>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Country</Form.Label>
                        <Form.Control
                            name="country"
                            required
                            type="text"
                            defaultValue={modalData?.country || ""}
                            onChange={(e) => { handleChange(e); setField('country', e.target.value); }}
                            placeholder="Example Denmark"
                            isInvalid={!!errors.country}>
                        </Form.Control>
                        <Form.Control.Feedback type='invalid'>
                            {errors.country}
                        </Form.Control.Feedback>
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

export default UserAddressModal