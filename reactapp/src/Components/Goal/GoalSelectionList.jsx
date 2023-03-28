
const GoalSelectionList = props => {

    return (
        <div className="d-flex flex-column flex-fill align-items-center wp-100 hp-100 min-h-0 p-2">
            <p>Goals:</p>
            <div className="d-flex flex-column flex-fill text-center overflow-y-scroll wp-100 goalList">
                {props.goals === null ? <div className="spinner-border align-self-center" role="status"/> :
                props.goals.length < 1 ? <p>No current goals..</p> :
                props.goals.map(goal => 
                    <div className="d-flex flex-column" key={goal.id}>
                        <input onChange={e => props.goalSelected(e, goal)} type={props.type} name="goal-list" id={`goal-${goal.id}`} className="btn-check"/>
                        <label htmlFor={`goal-${goal.id}`} className="btn btn-secondary border">Goal {goal.id}</label>
                    </div>
                )}
            </div>
        </div>
    )
}

export default GoalSelectionList