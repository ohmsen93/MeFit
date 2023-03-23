import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import Form from 'react-bootstrap/Form';

let update;

function UserAddressCard(props) {

    if (props.updateRequired != null || props.updateRequired != undefined) { handleUpdate(); console.log(props.updateRequired);}

    function handleOpenModal() {
        props.onModalOpen("UserAddressModal");
    }

    function handleUpdate() {
        console.log("UserAddressCard");
        update = props.updateRequired;
        handleAfterUpdate();
    }

    function handleAfterUpdate() {
        props.afterUpdate();
    }

    return (
        <Card>
            <Card.Header as="h5">Address Information</Card.Header>
            <Card.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address</Form.Label>
                        <Form.Control
                            readOnly
                            name="address"
                            defaultValue={update?.address || props.userData.adressData.addressLine1}
                            type="text"
                            placeholder="required">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 1</Form.Label>
                        <Form.Control
                            readOnly
                            name="addressSecond"
                            defaultValue={update?.addressSecond || props.userData.adressData.addressLine2}
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 2</Form.Label>
                        <Form.Control
                            readOnly
                            name="addressThird"
                            defaultValue={update?.addressThird || props.userData.adressData.addressLine3}
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Postal Code</Form.Label>
                        <Form.Control
                            readOnly
                            name="postalCode"
                            defaultValue={update?.postalCode || props.userData.adressData.postalCode}
                            required
                            type="number"
                            placeholder="Example : 8260 would be Viby j">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>City</Form.Label>
                        <Form.Control
                            readOnly
                            name="city"
                            defaultValue={update?.city || props.userData.adressData.city}
                            required
                            type="text"
                            placeholder="Example Viby j">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Country</Form.Label>
                        <Form.Control
                            readOnly
                            name="country"
                            defaultValue={update?.country || props.userData.adressData.country}
                            required
                            type="text"
                            placeholder="Example Denmark">
                        </Form.Control>
                    </Form.Group>
                </Form>
                <Button variant="primary" onClick={(e => handleOpenModal())}>Update Address</Button>
            </Card.Body>
        </Card>
    );
}

export default UserAddressCard