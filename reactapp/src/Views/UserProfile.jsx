import UserProfileForm from "../Components/profile/UserProfileForm";
import Background from "../Images/backgrounds/hd-squad-color.jpeg";

function UserProfile() {
    return (
        <>      
            <div className="bg">
                <img src={Background} alt=""/>
            </div>
            <div className="contentBox mt-5">
                <UserProfileForm />
            </div>       
        </>
    );
}

export default UserProfile;