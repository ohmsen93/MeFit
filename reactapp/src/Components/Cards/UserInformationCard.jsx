
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import Form from 'react-bootstrap/Form';

let update;

function UserProfileCard (props) {
    
    if (props.updateRequired != null || props.updateRequired != undefined) { handleUpdate(); console.log(props.updateRequired);}
    console.log(props.userData);
    function handleOpenModal (){
        props.onModalOpen("UserInformationModal");
    }
    
    function handleUpdate() {
        console.log("UserProfileCard");
        update = props.updateRequired;
        handleAfterUpdate();
        
    }

    function handleAfterUpdate() {
        props.afterUpdate();
    }

    return (
        <Card>
            <Card.Header as="h5">User Information</Card.Header>
            <Card.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>First Name</Form.Label>
                        <Form.Control
                            readOnly
                            name="firstName"
                            defaultValue={update?.firstName || props.userData.profileData.firstname}
                            type="text"
                            placeholder="first name">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Last Name</Form.Label>
                        <Form.Control
                            readOnly
                            name="lastName"
                            type="text"
                            defaultValue={update?.lastName || props.userData.profileData.lastname}
                            placeholder="last name">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Email</Form.Label>
                        <Form.Control
                            readOnly
                            name="email"
                            type="email"
                            defaultValue={update?.email || props.userData.profileData.email}
                            placeholder="example@gmail.com">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Phone Number</Form.Label>
                        <Form.Control
                            readOnly
                            name="phoneNumber"
                            type="tel"
                            defaultValue={update?.phoneNumber || props.userData.profileData.phone}
                            placeholder="45+11111111">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Profile picture</Form.Label>
                        <Form.Control
                            readOnly
                            name="profilePicture"
                            defaultValue={update?.profilePicture || props.userData.profileData.picture}
                            type="file">
                        </Form.Control>
                    </Form.Group>
                </Form>
                <Button variant="primary" onClick={(e => handleOpenModal())} >Update Personal</Button>
            </Card.Body>
        </Card>
    );
}

export default UserProfileCard