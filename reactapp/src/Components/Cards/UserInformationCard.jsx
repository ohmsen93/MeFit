
import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import Form from 'react-bootstrap/Form';

function UserProfileCard(props) {

    const [update, setUpdate] = useState();

    useEffect(() => {

        if (props.updateRequired != undefined || props.updateRequired != null) {
            setUpdate(props.updateRequired)
        }
        else if (props.userData.profileData != null || props.userData.profileData != undefined) {
            setUpdate(props.userData.profileData)
        }

    }, [props.updateRequired, props.userData.profileData])

    function handleOpenModal() {
        props.onModalOpen("UserInformationModal");
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
                            defaultValue={update?.firstName || update?.firstname}
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
                            defaultValue={update?.lastName || update?.lastname}
                            placeholder="last name">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Email</Form.Label>
                        <Form.Control
                            readOnly
                            name="email"
                            type="email"
                            defaultValue={update?.email}
                            placeholder="example@gmail.com">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Phone Number</Form.Label>
                        <Form.Control
                            readOnly
                            name="phoneNumber"
                            type="tel"
                            defaultValue={update?.phoneNumber || update?.phone}
                            placeholder="45+11111111">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Profile picture</Form.Label>
                        <Form.Control
                            readOnly
                            name="profilePicture"
                            defaultValue={update?.profilePicture || update?.picture}
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