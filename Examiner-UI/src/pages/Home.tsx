import { useNavigate } from 'react-router-dom';

export default function Home() {
  const navigate = useNavigate();

  const handleViewTags = () => {
    navigate('/tags?pageNumber=1&pageSize=10'); // customize this
  };

  return (
    <div>
      <h1>Welcome to Examiner</h1>
      <button onClick={handleViewTags}>View All Tags</button>
    </div>
  );
}
