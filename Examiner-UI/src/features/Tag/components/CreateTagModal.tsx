import type { TagDetailModel, TagFormModel } from "@/models/tags";
import { useState } from "react";
import { createTag } from "../TagApi";
import TagForm from "./TagForm";

type Props = {
  isOpen: boolean;
  onClose: () => void;
  onSuccess?: (tag: TagDetailModel) => void;
};

export default function CreateTagModal({ isOpen, onClose, onSuccess }: Props) {
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (data: TagFormModel) => {
    setLoading(true);
    try {
      const tag = await createTag(data);
      if(tag.isSuccess)
      {
        onSuccess?.(tag.value)
      }
      onClose();
    } finally {
      setLoading(false);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="modal-overlay">
      <div className="modal">
        <h2>Create Tag</h2>
        <TagForm onSubmit={handleSubmit} isLoading={loading} />
        <button onClick={onClose}>Cancel</button>
      </div>
    </div>
  );
}
