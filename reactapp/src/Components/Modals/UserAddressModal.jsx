import React, { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import keycloak from '../../keycloak';

function UserAddressModal(props) {

    const [show, setShow] = useState(props.requestOpen)
    const [modalData, setModalData] = useState({
        key: UserAddressModal.name,
        card: "UserAddressCard",
        address: "",
        addressSecond: "",
        addressThird: "",
        postalCode: "",
        city: "",
        country: ""

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

    function handleNext(event) {
        props.onHandleNext(event, modalData, modalData.key);
    }

    function handleSave(event) {
        props.onSave(event, modalData);
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
                            name="address"
                            required
                            type="text"
                            onChange={handleChange}
                            placeholder="required">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 1</Form.Label>
                        <Form.Control
                            name="addressSecond"
                            type="text"
                            onChange={handleChange}
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 2</Form.Label>
                        <Form.Control
                            name="addressThird"
                            type="text"
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
                        <Button variant="primary" onClick={(e => handleSave(e))}> Save Changes </Button>
                    </>
                }
            </Modal.Footer>
        </Modal>
    );
}

export default UserAddressModal