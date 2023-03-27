import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserAddressModal(props) {

    function defaultDataHandler() {
        // if it's the first time user is prompted to enter information available keycloak data will be used
        let modalData;
        
        if (props.onFirstLogin) {
            modalData = {
                key: UserAddressModal.name,
                card: "UserAddressCard",
                addressLine1: "",
                addressLine2: "",
                addressLine3: "",
                postalCode: "",
                city: "",
                country: ""
            }
            return modalData;
        }
        else {
            // check database for data to populate the modal
            modalData = {
                key: UserAddressModal.name,
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
        props.onHandleNext(event, modalData, modalData.key);
    }

    function handleSave() {
        props.onSave(modalData);
        handleClose();
    }

    return (
        <Modal show={show} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
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
                            onChange={handleChange}
                            placeholder="required">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 1</Form.Label>
                        <Form.Control
                            name="addressLine2"
                            type="text"
                            defaultValue={modalData?.addressLine2 || ""}
                            onChange={handleChange}
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 2</Form.Label>
                        <Form.Control
                            name="addressLine3"
                            type="text"
                            defaultValue={modalData?.addressLine3 || ""}
                            onChange={handleChange}
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Postal Code</Form.Label>
                        <Form.Control
                            name="postalCode"
                            required
                            type="number"
                            defaultValue={modalData?.postalCode || ""}
                            onChange={handleChange}
                            placeholder="Example : 8260 would be Viby j">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>City</Form.Label>
                        <Form.Control
                            name="city"
                            required
                            type="text"
                            defaultValue={modalData?.city || ""}
                            onChange={handleChange}
                            placeholder="Example Viby j">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Country</Form.Label>
                        <Form.Control
                            name="country"
                            required
                            type="text"
                            defaultValue={modalData?.country || ""}
                            onChange={handleChange}
                            placeholder="Example Denmark">
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

export default UserAddressModal