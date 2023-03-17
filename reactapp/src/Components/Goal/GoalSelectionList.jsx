
const GoalSelectionList = props => {

    return (
        <div className="d-flex flex-column flex-fill align-items-center border wp-100 min-h-0 p-2">
            <p>Goals:</p>
            {/* <GoalCreationContext.Consumer>
                {(programSelected) => ( */}
                    <div className="d-flex flex-column flex-fill text-center overflow-y-scroll wp-100">
                        {props.goals === "loading" ? <div className="spinner-border align-self-center" role="status"/> :
                        props.goals.map(goal => 
                            <div className="d-flex flex-column" key={goal.id}>
                                <input onChange={e => props.goalSelected(e, goal)} type={props.type} name="goal-list" id={`goal-${goal.id}`} className="btn-check"/>
                                <label htmlFor={`goal-${goal.id}`} className="btn btn-outline-secondary">Goal {goal.id}</label>
                            </div>
                        )}
                        {/* <input onChange={e => programSelected(e, {id: 1, name: "Program A"})} type="radio" name="program-list-radio" id="program-radio-1" className="btn-check"/>
                        <label htmlFor="program-radio-1" className="btn btn-outline-secondary">Program A</label>
                        <input onChange={e => programSelected(e, {id: 2, name: "Program B"})} type="radio" name="program-list-radio" id="program-radio-2" className="btn-check"/>
                        <label htmlFor="program-radio-2" className="btn btn-outline-secondary">Program B</label> */}
                    </div>
                {/* )}
            </GoalCreationContext.Consumer> */}
        </div>
    )
}

export default GoalSelectionList