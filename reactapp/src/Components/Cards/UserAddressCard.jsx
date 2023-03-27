import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import Form from 'react-bootstrap/Form';

let update;

function UserAddressCard(props) {

    const [update,setUpdate] = useState();
    
    useEffect(() => {

        console.log(props.userData);
        if (props.updateRequired != undefined){
            setUpdate(props.updateRequired)
        }
        else {
            setUpdate(props.userData.adressData)
        }
        
    },[props.updateRequired,props.userData.adressData])
    
    function handleOpenModal() {
        props.onModalOpen("UserAddressModal");
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
                            defaultValue={update?.address}
                            type="text"
                            placeholder="required">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 1</Form.Label>
                        <Form.Control
                            readOnly
                            name="addressSecond"
                            defaultValue={update?.addressSecond}
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Address 2</Form.Label>
                        <Form.Control
                            readOnly
                            name="addressThird"
                            defaultValue={update?.addressThird}
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Postal Code</Form.Label>
                        <Form.Control
                            readOnly
                            name="postalCode"
                            defaultValue={update?.postalCode}
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
                            defaultValue={update?.city}
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
                            defaultValue={update?.country}
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