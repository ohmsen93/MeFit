import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import Form from 'react-bootstrap/Form';

function UserFitnessCard(props) {
    
    const [update,setUpdate] = useState();
    
    useEffect(() => {

        if (props.updateRequired != undefined){
            setUpdate(props.updateRequired)
        }
        else {
            setUpdate(props.userData.profileData)
        }
        
    },[props.updateRequired,props.userData.profileData])
    
    function handleOpenModal() {
        props.onModalOpen("UserFitnessModal");
    }

    return (
        <Card>
            <Card.Header as="h5">Fitness Information</Card.Header>
            <Card.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Weight</Form.Label>
                        <Form.Control
                            readOnly
                            defaultValue={update?.weight}
                            name="weight"                           
                            type="number"
                            placeholder="weight in kg">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Height</Form.Label>
                        <Form.Control
                            readOnly
                            defaultValue={update?.height}
                            name="height"                       
                            type="number"
                            placeholder="height in cm">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>MedicalCondition</Form.Label>
                        <Form.Control as="textarea" rows={4}
                            readOnly
                            defaultValue={update?.medicalCondition}
                            name="medicalCondition"              
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="">
                        <Form.Label>Disabilities</Form.Label>
                        <Form.Control as="textarea" rows={4}
                            readOnly
                            defaultValue={update?.disabilities}
                            name="disabilities"
                            type="text"
                            placeholder="">
                        </Form.Control>
                    </Form.Group>
                </Form>
                <Button variant="primary" onClick={(e => handleOpenModal())}>Update Fitness</Button>
            </Card.Body>
        </Card>
    );
}

export default UserFitnessCard