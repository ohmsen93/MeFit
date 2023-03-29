import { useState } from "react"
import { postGoal } from "../../API/GoalAPI"

const GoalCreationForm = props => {
    const [state, setState] = useState({
        startDate: new Date().toLocaleDateString('en-US'),
        endDate: null,
    })
    
    const selectionNotice = () => {
        if (props.program !== null)
            return <div>{props.program.name}</div>
        else if (props.workouts.length > 0)
            return (
                <div className="overflow-y-scroll text-center hp-100">
                {props.workouts.map(w => <div key={w.id}>{w.name}</div>)}
                </div>
            )
        else
            return <p>No program or workouts selected...</p>
    }
    const submitGoal = (event) => {
        event.preventDefault()
        const programId = props.program?.id || null
        const workoutIds = props.workouts.map(w => w.id)
        let goal = {
            startDate: event.target[0].value,
            endDate: event.target[1].value,
            fkTrainingprogramId: null,
            workouts: []
        }
        if (programId !== null) { // POST GoalByProgram
            goal = {...goal, fkTrainingprogramId: programId}
            postGoal(goal)
            console.log(goal)
        }
        else if (workoutIds.length > 0) { // POST GoalByWorkouts
            goal = {...goal, workouts: workoutIds}
            postGoal(goal)
            console.log(goal)
        }
        else { alert("Required: Select a program or workouts") }
    }

    return (
        <>
        <h3>Create new goal</h3>
        <div className="hp-100 p-2">
            {/* <GoalCreationContext.Consumer>
            {({program, workouts}) => ( */}
                    <>
                    {selectionNotice()}
                    {/* {props.program !== null && <div>{props.program.name}</div>}
                    {props.workouts.length > 0 &&
                        <div className="overflow-y-scroll text-center hp-100">
                            {props.workouts.map(w => <div key={w.id}>{w.name}</div>)}
                        </div>
                    } */}
                    </>
            {/* )}
            </GoalCreationContext.Consumer> */}
        </div>
        <form onSubmit={submitGoal} className="d-flex flex-column gap-3 hp-50">
            <div>
                <label htmlFor="start-date">Start date:</label>
                <br/>
                <input onChange={e => setState({...state, startDate: e.target.value})} type="date" min={new Date().toLocaleDateString('fr-ca')} id="start-date" required/>
            </div>
            <div>
                <label htmlFor="end-date">End date:</label>
                <br/>
                <input onChange={e => setState({...state, endDate: e.target.value})} type="date" min={state.startDate} id="start-date" required/>
            </div>
            <div>
                <button type="submit" className="btn btn-secondary border">Save goal</button>
            </div>
        </form>
        </>
    )
}

export default GoalCreationForm